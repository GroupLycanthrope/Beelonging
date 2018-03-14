using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearPaw : MonoBehaviour {

    bool bDoOnce;

    public Vector3 v3TargetPos;
    Vector3 v3BeGoneThoot;
    float fSpeed = 6;

    float fTimer;
    float fClock;

    private Animator aAnimator;

    float fSoundClock;

    private AudioSource source;

    GameObject Hit;

    GameObject[] Storage;

    // Use this for initialization
    void Start () {
        aAnimator = GetComponent<Animator>();
        Hit = GameObject.FindGameObjectWithTag("Dead");
        v3BeGoneThoot = gameObject.transform.position;
        source = gameObject.GetComponent<AudioSource>();
        v3BeGoneThoot.y = -20;
        fClock = 0.3f;
        fTimer = fClock;

        fSoundClock = 2.48f;
        bDoOnce = false;
        Storage = GameObject.FindGameObjectsWithTag("Storage");

    }

    // Update is called once per frame
    void Update () {
        if (Menu.bClawActivation) {
            if(Hit!= null){
                transform.position = Vector3.MoveTowards(transform.position, v3TargetPos, fSpeed * Time.fixedDeltaTime);
            }

            if (RotateHoneycomb.bHasCollided) {

                if(fSoundClock > 0 && !bDoOnce) {
                    source.Play();
                    bDoOnce = true;
                }

                if(fSoundClock <= 0) {
                    source.Stop();
                }
                fSoundClock -= Time.deltaTime;
            }

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
                if(Hit != null && Hit.transform.position.x <= -7) {
                    Destroy(Hit);
                }
            }
            if (Hit == null){

                transform.position = Vector3.MoveTowards(transform.position, v3BeGoneThoot, (fSpeed * Time.fixedDeltaTime) / 2);
            }

        }
	}
}
