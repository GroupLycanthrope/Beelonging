using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeManager : MonoBehaviour {

    public GameObject goPrefabPlayer;

    public GameObject goPlayer;

    public GameObject[] Swarm;
    public bool HoneyFormation = false;
    public bool bNoPlayer = false;
	bool bPlayerShootFired;

    public static int iPowerUpCounter;

    public static bool bIsInvincible;

    public GameObject xGameOverScreen;


    // Use this for initialization
    void Start() {
        iPowerUpCounter = 0;
        Swarm = GameObject.FindGameObjectsWithTag("Bee");
        iPowerUpCounter = 0;
        bIsInvincible = false;
    }

    // Update is called once per frame
    void Update() {

        for (int i = 0; i < Swarm.Length; i++) { 
            if (Swarm[i] != null){
                if (Swarm[i].GetComponent<ControlHealth>().getHealth() == 0){
                    Destroy(Swarm[i]);
                }
            }
        }
        Swarm = GameObject.FindGameObjectsWithTag("Bee");

        Formation();

        // respawn of player
        Respawn();
        




        if (iPowerUpCounter == 3){
            bIsInvincible = true;
            InvincibilityCounter.fInvincibilityTimer -= Time.deltaTime;
        }

        if (InvincibilityCounter.fInvincibilityTimer <= 0){
            bIsInvincible = false;
            InvincibilityCounter.fInvincibilityTimer = 5.0f;
            iPowerUpCounter = 0;
        }

        if(Swarm.Length == 0) {
            //Time.timeScale = 0;
            xGameOverScreen.SetActive(true);
        }

    }

    void Respawn() {
        for (int i = 0; i < Swarm.Length; i++){
            if (Swarm[i].layer == 8){
                bNoPlayer = false;
                break;
            }
            else{
                bNoPlayer = true;
            }
        }
        if (bNoPlayer == true) {
            int rand = Random.Range(1,Swarm.Length);
            // Save cordinates of old AIbee
            Vector3 cordinates = Swarm[rand - 1].transform.position;
            // destroy the AI bee
            Destroy(Swarm[rand - 1]);
            // Create a player
            GameObject newPlayer = Instantiate(goPrefabPlayer);
            newPlayer.name = "Player";
            newPlayer.transform.position = cordinates;
        }
    }

        void Formation() {
        for (int i = 0; i < Swarm.Length; i++){
            if (Input.GetKey("x") && Swarm[i].name != "Player"){
                Swarm[i].GetComponent<AIBee>().bCallFormation = true;
                Swarm[i].GetComponent<AIBee>().bPlayerFireShoot = bPlayerShootFired;
            }
            else if (Swarm[i].name != "Player"){
 //               Swarm[i].GetComponent<AIBee>().bCallFormation = false;
            }
            if (Swarm[i].name == "Player"){
                bPlayerShootFired = Swarm[i].GetComponent<PlayerController>().bShootFired;
            }
        }
    }
}