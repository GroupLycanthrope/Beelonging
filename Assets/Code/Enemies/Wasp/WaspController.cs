using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspController : MonoBehaviour {

    public GameObject xProjectile;
    public GameObject xProjectileOrigin;

    int iNewDirection;

    Vector3 v3OriginalPosition;
    Vector3 v3Direction;

// Move Floats
    public float fMoveSpeed;
    public float fRadius;
    public float fCooldownBeforeNextMove;
    float fMoveTimer;
    // Fire floats
    public float fStandStillBeforeFire;
    public float fFireRate;
    float fFireTimer;
// Fire Bools
    bool bWantToFire;
    bool bStopMove;
    bool bHasFired;
// Move Bools
    bool bChangeDirection;
    bool bCanMove;
    bool bHasNewDirection;

// initial move
    bool bInitialMove;
    bool bInitialPosDecided;
    float fInitialPos;

    WaspCollision wasp;

    // Use this for initialization
    void Start () {
        bInitialMove = true;
        bInitialPosDecided = false;
        wasp = GetComponent<WaspCollision>();
        fMoveTimer = fCooldownBeforeNextMove;
        bCanMove = true;
    }
	
	// Update is called once per frame
	void Update () {
        // decides if the wasp should move to a position before it starts it random movement and fire
        if (!bInitialMove) {
            // it want to fire so it stops moving
            if (bWantToFire){
                bStopMove = true;
                // it fires
                if(wasp.GetHealth() > 0 && fFireTimer <= 0) {
                    GameObject WaspBullet = Instantiate(xProjectile);
                    WaspBullet.transform.position = xProjectileOrigin.transform.position;
                    fFireTimer = fFireRate;
                    bHasFired = true;
                }
            }
            else {
                //it has not fired
                bHasFired = false;
            }

            if (bHasFired) {
                //if it has fired it can move again
                bStopMove = false;
            }       

            if(fFireTimer <= fStandStillBeforeFire) {
                // starts to stand still because it want to fire
                bWantToFire = true;
            }
            else {
                bWantToFire = false;
            }

            if (!bStopMove) {
                if (bCanMove){
                    GetBorders();
                }

                if (bCanMove && !bChangeDirection){
                    MoveWasp();
                }
                if (bChangeDirection && !bCanMove){
                    SetRandomDirection();
                }
            }
            fFireTimer -= Time.deltaTime;
        }
        else {
            // gets a random pos to move to it
            if (!bInitialPosDecided){
                fInitialPos = Random.Range(2f, 7f);
                bInitialPosDecided = true;
            }
            // moves the wasp to the decided position
            if (transform.position.x >= fInitialPos){
                transform.Translate(Vector2.left * fMoveSpeed * Time.deltaTime);
            }
            // the wasp has reached its position and will start it random movement and fire straight forward
            else{
                bInitialMove = false;
                v3OriginalPosition = transform.position;
            }
        }
    }

    void MoveWasp() {
// dont have enough enery to write what all of them do. Just move the wasp in different direction
        if(iNewDirection == 0) {
            transform.Translate(Vector2.left * fMoveSpeed * Time.deltaTime);
        }
        if(iNewDirection == 1) {
            transform.Translate(Vector2.right * fMoveSpeed * Time.deltaTime);
        }
        if (iNewDirection == 2){
            transform.Translate(Vector2.up * fMoveSpeed * Time.deltaTime);
        }
        if (iNewDirection == 3){
            transform.Translate(Vector2.down * fMoveSpeed * Time.deltaTime);
        }
        if (iNewDirection == 4){
            transform.Translate(Vector2.left * fMoveSpeed * Time.deltaTime);
            transform.Translate(Vector2.up * fMoveSpeed * Time.deltaTime);
        }
        if (iNewDirection == 5){
            transform.Translate(Vector2.left * fMoveSpeed * Time.deltaTime);
            transform.Translate(Vector2.down * fMoveSpeed * Time.deltaTime);
        }
        if (iNewDirection == 6){
            transform.Translate(Vector2.right * fMoveSpeed * Time.deltaTime);
            transform.Translate(Vector2.up * fMoveSpeed * Time.deltaTime);
        }
        if (iNewDirection == 7){
            transform.Translate(Vector2.right * fMoveSpeed * Time.deltaTime);
            transform.Translate(Vector2.down * fMoveSpeed * Time.deltaTime);
        }
    }

    void SetRandomDirection() {
        fMoveTimer -= Time.deltaTime;

        if (!bHasNewDirection) {
            // randomices an int for the future when it goes back to moveWasp
            iNewDirection = Random.Range(0, 7);
            bHasNewDirection = true;
        }
        // it move back while waiting to move to be able to move to another random position
        transform.position = Vector3.MoveTowards(transform.position, v3OriginalPosition, fMoveSpeed * Time.deltaTime);
        // the wasp starts to move to another position
        if (fMoveTimer <= 0) {
            fMoveTimer = fCooldownBeforeNextMove;
            bCanMove = true;
            bChangeDirection = false;
        }
    }

    void GetBorders() {
        if(Vector3.Distance(transform.position, v3OriginalPosition) < fRadius) {
            // if the wasp is within the radius it will move
            bCanMove = true;
        }
        else {
            // it is outside the range and starts to move back
            bHasNewDirection = false;
            bCanMove = false;
            bChangeDirection = true;
        }
    }
}
