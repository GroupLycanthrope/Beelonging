﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeManager : MonoBehaviour {

    public GameObject goPlayer;

    public GameObject[] PlayerHitBoxes;

    public GameObject[] Swarm;
    public bool HoneyFormation = false;

    public static int iPowerUpCounter;

    public static bool bIsInvincible;

    // Use this for initialization
    void Start() {
        Swarm = GameObject.FindGameObjectsWithTag("Bee");
        bIsInvincible = false;
    }

    // Update is called once per frame
    void Update() {
        Formation();
        for (int i = 0; i < Swarm.Length; i++) { 
            if (Swarm[i] != null){
                if (Swarm[i].GetComponent<ControlHealth>().getHealth() == 0){
                    Destroy(Swarm[i]);
                }
            }
        }
        Swarm = GameObject.FindGameObjectsWithTag("Bee");
 
        if (iPowerUpCounter == 3)
        {
            bIsInvincible = true;
            InvincibilityCounter.fInvincibilityTimer -= Time.deltaTime;
        }

        if (InvincibilityCounter.fInvincibilityTimer <= 0)
        {
            bIsInvincible = false;
            InvincibilityCounter.fInvincibilityTimer = 5.0f;
            iPowerUpCounter = 0;
        }
    }
    int i = 0;

    void Formation() {
        // make that the AI bees have a destination to move to
        if (Swarm[i].name != "Player" && HoneyFormation == true && Swarm[i] != null){
            Swarm[i].GetComponentInChildren<Find_Move>().v3PlayerPos = goPlayer.transform.position;
        }

        // Sets the honeyFormation to true and calls for all of the bees
        if (Input.GetKey("z") && Swarm[i].name != "Player" && Swarm[i] != null && i < Swarm.Length) {
            HoneyFormation = true;
            Swarm[i].GetComponentInChildren<Find_Move>().BeeCallerFormation = true;
        }
        else if(Swarm[i].name != "Player" && Swarm[i] != null && i < Swarm.Length) {
            HoneyFormation = false;
            Swarm[i].GetComponentInChildren<Find_Move>().BeeCallerFormation = false;
        }
        // The Player collider gets smaller here
        if (i == 0 && HoneyFormation == true && Swarm[i] != null && i < Swarm.Length)
        {
            PlayerHitBoxes[i].GetComponent<CircleCollider2D>().radius = 2f;
        }
        else if (i == 1 && HoneyFormation == true && Swarm[i] != null && i < Swarm.Length)
        {
            PlayerHitBoxes[i].GetComponent<CircleCollider2D>().radius = 2;
        }
        // Sets the detection for AI bees to one
        if (Swarm[i].name != "Player" && HoneyFormation == true && Swarm[i] != null && i < Swarm.Length){
            
            Swarm[i].GetComponentInChildren<CircleCollider2D>().radius = 2;
        }
        else if (Swarm[i].name != "Player" && HoneyFormation == false && Swarm[i] != null && i < Swarm.Length){
            Swarm[i].GetComponentInChildren<CircleCollider2D>().radius = 6;
        }
        i++;
        if(i >= Swarm.Length) {
            i = 0;
        }
        
        
        // revert back the player collider to normal
        if (HoneyFormation == false && i == 0) {
            PlayerHitBoxes[i].GetComponent<CircleCollider2D>().radius = 4;
            PlayerHitBoxes[i+1].GetComponent<CircleCollider2D>().radius = 3;
        }

    }
}