using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyMovement : MonoBehaviour
{
    int iZoomCount;

    public float fFlyingSpeed;
    public float fZoomSpeed;
    public float fZoomCooldown;
    public float fRangeX;
    public float fRangeY;

    private float fTimer;
    private float fRandomX;
    private float fRandomY;
    private float fPointX;
    private float fPointY;

    Vector3 v3ZoomDestination;
    Vector3 v3OriginalYPosition;
    Vector3 v3NormalMoveSpeed;

    bool bIsAtTarget;
    bool bRestartTimer;
    bool bHasTarget;
    bool bStartMoveToScreen;


    // Use this for initialization
    void Start(){
        v3OriginalYPosition.y = transform.position.y;
        fTimer = fZoomCooldown;
        bStartMoveToScreen = true;
        v3NormalMoveSpeed.x = -fFlyingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -20){
            Destroy(gameObject);
        }
        fTimer -= Time.deltaTime;

        if(transform.position.x > 8) {
            transform.position += v3NormalMoveSpeed;
        }
        else {
            bStartMoveToScreen = false;
        }

        if(!bStartMoveToScreen) {
            Zoom();
        }
        if (bHasTarget) {
            MoveDragonFly();
        }


        if (fTimer <= 0){
            print("check");
            bRestartTimer = true;
        }
    }
    
    void Zoom() {
        
        if (iZoomCount == 0 && fTimer <= 0) {
            v3ZoomDestination = SetZoomPos();

            if(v3ZoomDestination.y >= 5.4F) {
                v3ZoomDestination.y -= 1;
            }
            if(v3ZoomDestination.y <= -5.4) {
                v3ZoomDestination.y += 1;
            }
            bHasTarget = true;
            iZoomCount += 1;
        }
        else if(iZoomCount == 1 && fTimer <= 0 && bIsAtTarget == true) {
            Vector3 v3SaveDesti = v3ZoomDestination;
            v3ZoomDestination = SetZoomPos();
            v3ZoomDestination.y = v3SaveDesti.y * -1;
            if (v3ZoomDestination.y >= 5.4F){
                v3ZoomDestination.y -= 1;
            }
            if (v3ZoomDestination.y <= -5.4){
                v3ZoomDestination.y += 1;
            }
            bHasTarget = true;
            iZoomCount += 1;
        }
        else if (iZoomCount == 2 && fTimer <= 0 && bIsAtTarget == true) {
            v3ZoomDestination = SetZoomPos();
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

        fRandomX = Random.Range(transform.position.x - 1, fPointX);
        fRandomY = Random.Range(transform.position.y - .5F, fPointY);

        v2ZoomPos.x = fRandomX;
        v2ZoomPos.y = fRandomY;

        return v2ZoomPos;
    }


    void MoveDragonFly() {
        if(transform.position.x > v3ZoomDestination.x) {
            print("test");
            transform.position = Vector3.MoveTowards(transform.position, v3ZoomDestination, fZoomSpeed * Time.deltaTime);
            bIsAtTarget = false;
        }
        else if (transform.position.x <= v3ZoomDestination.x){
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
