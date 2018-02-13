using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBee : MonoBehaviour {

    private GameObject goPlayer;

    public GameObject goFormationPosition;

    public GameObject goPrevFormPos;

    public GameObject[] goListOfFormationPos;


    RaycastHit2D rhPlayerSight;
    RaycastHit2D rhCheckPosition;

    PositionInFormation pifFormation;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (goPlayer == null) {
            goPlayer = GameObject.Find("Player");
        }
        if(goFormationPosition == null) {
            goListOfFormationPos = GameObject.FindGameObjectsWithTag("Formation");

            goFormationPosition = GameObject.FindGameObjectWithTag("Formation");
            goPrevFormPos = goFormationPosition;
        }

        if(goPlayer != null) {
            rhPlayerSight = Physics2D.Linecast(transform.position, goPlayer.transform.position);
            Debug.DrawLine(transform.position, goPlayer.transform.position, Color.green);
        }

        if(goFormationPosition != null) {
            rhCheckPosition = Physics2D.Linecast(transform.position, goFormationPosition.transform.position);
            Debug.DrawLine(transform.position, goFormationPosition.transform.position, Color.red);

            if(rhCheckPosition.collider.tag == "Formation") {
                if( pifFormation == null) {
                    pifFormation = rhCheckPosition.collider.GetComponent<PositionInFormation>();
                }

                if (pifFormation.sNameOfBee == "" || pifFormation.sNameOfBee == gameObject.name && pifFormation.bIsOccupied == true) {
                    pifFormation.bIsOccupied = true;
                    pifFormation.sNameOfBee = gameObject.name;
                }
                else {
                    for(int i = 0; i < goListOfFormationPos.Length; i++) {
                        if (goListOfFormationPos[i].GetComponent<PositionInFormation>().bIsOccupied == false) {
                            goFormationPosition = goListOfFormationPos[i];
                            pifFormation = null;
                            break;
                        }
                    }
                }
                for (int i = 0; i < goListOfFormationPos.Length; i++){
                    if (goListOfFormationPos[i].GetComponent<PositionInFormation>().sNameOfBee == this.name) {

                        goFormationPosition = goListOfFormationPos[i];
                        pifFormation = null;
                        return;
                    }
                    else if (goListOfFormationPos[i].GetComponent<PositionInFormation>().bIsOccupied == false ){
                        print("this is done by " + this.name);
                        goFormationPosition = goListOfFormationPos[i];
                        pifFormation = null;
                        break;
                    }
                }
            }
        }

	}
}
