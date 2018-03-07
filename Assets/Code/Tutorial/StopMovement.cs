using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovement : MonoBehaviour {

    public Vector3 v3StopToMove;

    bool bStopNow;

	// Use this for initialization
	void Start () {
        bStopNow = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x <= v3StopToMove.x && !bStopNow) {
            if (gameObject.GetComponent<CircleCollider2D>()) {
                transform.gameObject.GetComponent<Honeycomb>().fFloatingSpeed = 0;
                bStopNow = true;
            }
            if (gameObject.GetComponent<CapsuleCollider2D>()) {
                transform.gameObject.GetComponent<GoldenBeePickUp>().fMoveSpeed = 0;
                bStopNow = true;
            }
        }
           
	}
}
