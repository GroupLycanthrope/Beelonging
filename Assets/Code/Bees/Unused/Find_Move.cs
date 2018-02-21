using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Find_Move : MonoBehaviour {

   public GameObject goParent;
   bool bUp,bDown,bRight,bLeft;
   bool bBorderUp, bBorderDown, bBorderRight, bBorderLeft;
   public bool BeeCallerFormation;
   public bool InFormation;
   public Vector3 v3PlayerPos;

    Vector3 v3Pos;
    public float fSpeed = 0.01f;
    public float fAcceleration;

	// Use this for initialization
	void Start () {
        fAcceleration = fSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        v3Pos = goParent.transform.position;
        if (bLeft == true && bBorderLeft == false) {
            v3Pos.x -= fAcceleration;
        }
        else {
            v3Pos.x += fAcceleration;
        }
        if (bRight == true && bBorderRight == false){
            v3Pos.x += fAcceleration;
        }
        else {
            v3Pos.x -= fAcceleration;
        }
        if (bUp == true && bBorderUp == false){
            v3Pos.y += fAcceleration;
        }
        else {
            v3Pos.y -= fAcceleration;
        }
        if (bDown == true && bBorderDown == false){
            v3Pos.y -= fAcceleration;
        }
        else {
            v3Pos.y += fAcceleration;
        }
        if(BeeCallerFormation == true && InFormation == false) {
            if(fAcceleration == fSpeed){
                fAcceleration *= 10;
            }
            if(goParent.transform.position.x < v3PlayerPos.x) {
                v3Pos.x += fAcceleration;
            }
            else if (goParent.transform.position.x > v3PlayerPos.x){
                v3Pos.x -= fAcceleration;
            }
            if (goParent.transform.position.y < v3PlayerPos.y){
                v3Pos.y += fAcceleration;
            }
            else if (goParent.transform.position.y > v3PlayerPos.y){
                v3Pos.y -= fAcceleration;
            }
        }
        else {
            fAcceleration = fSpeed;
        }

        goParent.transform.position = v3Pos;
	}

    private void OnTriggerStay2D(Collider2D other){
        if (other.tag == "LowPrioPlayer" || other.tag == "Bee") {
            if(goParent.transform.position.x < other.transform.position.x) {
                bLeft = true;
                bRight = false;
            }
            if (goParent.transform.position.y < other.transform.position.y){
                bDown = true;
                bUp = false;
            }
            if (goParent.transform.position.x > other.transform.position.x){
                bRight = true;
                bLeft = false;
            }
            if (goParent.transform.position.y > other.transform.position.y){
                bUp = true;
                bDown = false;
            }   
        }
        if (other.tag == "Border") {
            if (other.gameObject.name == "Up") {
                bBorderUp = true;
                bBorderDown = false;
            }
            if (other.gameObject.name == "Down") {
                bBorderUp = false;
                bBorderDown = true;
            }
            if (other.gameObject.name == "Right") {
                bBorderRight = true;
                bBorderLeft = false;
            }
            if (other.gameObject.name == "Left") { 
                bBorderLeft = true;
                bBorderRight = false;
            }
        }
        if(other.name == "Formation") {
            InFormation = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            fAcceleration *= 5;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(bLeft == true) {
            bLeft = false;
        }
        if (bDown == true){
            bDown = false;
        }
        if (bRight == true){
            bRight = false;
        }
        if (bUp == true){
            bUp = false;
        }
        fAcceleration = fSpeed;
        if (other.name == "Formation")
        {
            InFormation = false;
        }
    }
}
