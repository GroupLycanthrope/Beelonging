using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyMovement : MonoBehaviour
{
    //Count on what zoom it is on
    int iZoomCount;
    // starting speed it moves on the screen with
    public float fFlyingSpeed;
    //ZOOOOOOOOOOOOOM SPEEED!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public float fZoomSpeed;
    //ZOOOOOOOOOOOOOOM COOOOOLDOWN!!!!!!!!!
    public float fZoomCooldown;
    // Max Zoom in X-position
    public float fRangeX;
    //Max Zoom in Y-position
    public float fRangeY;

    public float fMaxWaitTimeBeforeZooming;

    private float fWaitTime;
    //Clock to next zoom
    private float fTimer;
    // get the random point to zoom to
    private float fRandomX;
    private float fRandomY;
    private float fPointX;
    private float fPointY;
    // The destination on the ZOOOOOOOOOOOOOOOOOOOOOOOOOOOM
    Vector3 v3ZoomDestination;
    //the name says it all
    Vector3 v3OriginalYPosition;
    // had to add a vector so the damn thing could move
    Vector3 v3NormalMoveSpeed;

    // checks if the dragonfly is at the targeted destination so next zoom can be initated
    bool bIsAtTarget;
    // this becomes true when the dragonfly hits it target
    bool bRestartTimer;
    // if it has a target to move to (zooom)
    bool bHasTarget;
    // so it does not start to zoom while it moves normaly on to the screen
    bool bStartMoveToScreen;


    // Use this for initialization
    void Start(){
        fWaitTime = Random.Range(0f, fMaxWaitTimeBeforeZooming);
        v3OriginalYPosition.y = transform.position.y;
        fTimer = fZoomCooldown;
        bStartMoveToScreen = true;
        v3NormalMoveSpeed.x = -fFlyingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        fWaitTime -= Time.deltaTime;
        //only remenant of Fredriks code RIP code ;(
        if (transform.position.x < -15){
            Destroy(gameObject);
        }
        // clocker
        fTimer -= Time.deltaTime;
        // normal flying
        if(transform.position.x > 7) {
            transform.position += v3NormalMoveSpeed;
        }
        else { 
            bStartMoveToScreen = false;
        }

        if(!bStartMoveToScreen) {
            if(fWaitTime <= 0) {
                Zoom();
            }
        }
        if (bHasTarget) {
            MoveDragonFly();
        }


        if (fTimer <= 0){
            bRestartTimer = true;
        }
    }
    
    void Zoom() {
        
        if (iZoomCount == 0 && fTimer <= 0) {
            v3ZoomDestination = SetZoomPos();

            // checks so it is not out of bounds
            if(v3ZoomDestination.y >= 5.4F) {
                v3ZoomDestination.y -= 1;
            }
            if(v3ZoomDestination.y <= -5.4) {
                v3ZoomDestination.y += 1;
            }
            // omg it has a target to zoom to
            bHasTarget = true;
            iZoomCount += 1;
        }
        else if(iZoomCount == 1 && fTimer <= 0 && bIsAtTarget == true) {
            // right I only want to save the Y axis so I can add it with -1 to get it to move to the other way later in the script other wise it is copied from before
            Vector3 v3SaveDesti = v3ZoomDestination;
            v3ZoomDestination = SetZoomPos();
            // here
            v3ZoomDestination.y = v3SaveDesti.y * -1;
            if (v3ZoomDestination.y >= 4.6F){
                v3ZoomDestination.y -= 1;
            }
            if (v3ZoomDestination.y <= -4.6F){
                v3ZoomDestination.y += 1;
            }
            bHasTarget = true;
            iZoomCount += 1;
        }
        else if (iZoomCount == 2 && fTimer <= 0 && bIsAtTarget == true) {
            // this is copied before
            v3ZoomDestination = SetZoomPos();
            // haha almost got you... it goes back to the original Y pos, because design fluff.
            v3ZoomDestination.y = v3OriginalYPosition.y;
            bHasTarget = true;
            iZoomCount += 1;
        }
    }

    Vector2 SetZoomPos() {
        Vector2 v2ZoomPos;

        // fPoint decide how large each jump is;
        fPointX = transform.position.x - fRangeX;
        fPointY = transform.position.y - fRangeY;

        //Randomnes magic happens here
        fRandomX = Random.Range(transform.position.x - 1, fPointX);
        fRandomY = Random.Range(transform.position.y - .5F, fPointY);

        v2ZoomPos.x = fRandomX;
        v2ZoomPos.y = fRandomY;

        return v2ZoomPos;
    }


    void MoveDragonFly() {
        if(transform.position.x > v3ZoomDestination.x) {
            // this code snippet is my darling I love it sooo much
            transform.position = Vector3.MoveTowards(transform.position, v3ZoomDestination, fZoomSpeed * Time.deltaTime);
            // had to add this part because unity is a bich and did not want to do as I wanted to at first
            bIsAtTarget = false;
        }
        else if (transform.position.x <= v3ZoomDestination.x){
            // the reset logic so it zooms again later. It is like a cluster fuck but I do not have enough energy to fix it so joke on you :D
            if(bRestartTimer == true) {
                fTimer = fZoomCooldown;
                bRestartTimer = false;
            }
            bHasTarget = false;
            bIsAtTarget = true;

            if (iZoomCount == 3){
                iZoomCount = 0;
            }
        }
    }
}
