using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RotateHoneycomb : MonoBehaviour {

    HingeJoint2D Pendilum;

    bool bTurnOnPendilum;
    bool bTurnOffPendilum;
    static public bool bHasCollided;

    float fTimer;
    float fClock;

    public AudioClip acNomNomNom;

    private AudioSource asSource;
	// Use this for initialization
	void Start () {
        asSource = GetComponent<AudioSource>();
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
            asSource.PlayOneShot(acNomNomNom);
            bHasCollided = true;
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        bHasCollided = false;
    }
}
