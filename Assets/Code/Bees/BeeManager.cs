using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeManager : MonoBehaviour {

    public GameObject m_xPlayer;

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

    void Formation() {
        
    }


}