﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateHoneycomb : MonoBehaviour {

    HingeJoint2D Pendilum;

    bool bTurnOnPendilum;
    bool bTurnOffPendilum;
    bool bHasCollided;

    float fTimer;
    float fClock;
	// Use this for initialization
	void Start () {
        Pendilum = gameObject.GetComponent<HingeJoint2D>();
        bTurnOnPendilum = false;
        bTurnOffPendilum = false;
        fClock = 0.3f;
        fTimer = fClock;
    }
	
	// Update is called once per frame
	void Update () {
        if (bHasCollided) {
            if (!bTurnOnPendilum) {
                Pendilum.useMotor = true;
                bTurnOnPendilum = true;
            }
            fTimer -= Time.deltaTime;
            if(fTimer <= 0 && !bTurnOffPendilum) {
                Pendilum.useMotor = false;
                bTurnOffPendilum = true;
                fTimer = 6;
            }
            if(fTimer <= 0) {
                Menu.bClawActivation = false;
                 SceneManager.LoadScene("Level1");
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D p_xOther){
        if (p_xOther.CompareTag("Dead")) {
            bHasCollided = true;
        }
    }

}