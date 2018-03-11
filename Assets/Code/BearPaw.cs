using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearPaw : MonoBehaviour {


    public Vector3 v3TargetPos;
    float fSpeed = 6;

    float fTimer;
    float fClock;

    private Animator aAnimator;

    GameObject Hit;

    GameObject[] Storage;

    // Use this for initialization
    void Start () {
        aAnimator = GetComponent<Animator>();
        Hit = GameObject.FindGameObjectWithTag("Dead");

        fClock = 0.3f;
        fTimer = fClock;

        Storage = GameObject.FindGameObjectsWithTag("Storage");

    }

    // Update is called once per frame
    void Update () {
        //if(Storage.Length > 1) {
            
        //    if (Storage[0] != null && Storage[0].GetComponent<TutorialStorage>().GetTutorialStatus()) {
        //        Destroy(Storage[0]);
        //        Storage = GameObject.FindGameObjectsWithTag("Storage");
        //    }
        //    else {
        //        if(Storage[1] != null) {
        //            Destroy(Storage[1]);
        //            Storage = GameObject.FindGameObjectsWithTag("Storage");
        //        }
        //    }
        //}


        if (Menu.bClawActivation) {
            transform.position = Vector3.MoveTowards(transform.position, v3TargetPos, fSpeed * Time.fixedDeltaTime);

            if(transform.position == v3TargetPos) {
                aAnimator.SetTrigger("tIsAtPosition");
                
                if(fTimer <= 0) {
                    if(Hit != null) {
                        Hit.transform.Translate(-fSpeed * Time.fixedDeltaTime, 0, 0);
                    }
                   
                }
                else {
                    Hit.transform.Translate(0, 0, 0);
                    fTimer -= Time.fixedDeltaTime; 
                }
                if(Hit != null && Hit.transform.position.x <= -10) {
                    Destroy(Hit);
                }
            }
        }
	}
}
