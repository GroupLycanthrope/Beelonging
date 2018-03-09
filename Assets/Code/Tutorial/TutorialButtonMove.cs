using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtonMove : MonoBehaviour {

    public Vector3 v3TargetPos;
    public Vector3 v3OffScreen;
    public GameObject[] TutorialButtons;

    public float fMoveSpeed;


    public bool bDoNotStopMove;
    bool bAllPressed;
    bool bDoOnce;

    // Use this for initialization
    void Start () {
        v3TargetPos.x = 0;
        v3TargetPos.y = 2;
        v3TargetPos.z = 0;

        v3OffScreen.x = -20;
        v3OffScreen.y = 2;
        v3OffScreen.z = 0;
        TutorialButtons = GameObject.FindGameObjectsWithTag("Tutorial");

        bDoOnce = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!bDoNotStopMove) {
            if (transform.position.x >= 0){
                float step = fMoveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, v3TargetPos, step);
               
            }
            if( transform.position.x == 0) {
                if (!bDoOnce) {
                    foreach (GameObject obj in TutorialButtons){
                        if(obj != null) {
                            obj.GetComponent<TutorialButton>().SetIsAtPos(true);
                        }
                    }
                    bDoOnce = true;
                }
            }
            else {
                if (bDoOnce) {
                    foreach (GameObject obj in TutorialButtons){
                        if(obj != null) {
                            obj.GetComponent<TutorialButton>().SetIsAtPos(false);
                        }
                    }
                }
                
            }
            if (bAllPressed){
                float step = fMoveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, v3OffScreen, step);
            }
            if (transform.position.x == v3OffScreen.x){
                Destroy(gameObject);
            }
        }
        else {
            float step = fMoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, v3OffScreen, step);
        }
	   

        foreach (GameObject obj in TutorialButtons) {
            if(obj != null ) {
                if (!obj.GetComponent<TutorialButton>().GetIsPressed()){
                    bAllPressed = false;
                    break;
                }
                if (obj.GetComponent<TutorialButton>().GetIsPressed()){
                    bAllPressed = true;
                }
            }
            
        }

	}
}
