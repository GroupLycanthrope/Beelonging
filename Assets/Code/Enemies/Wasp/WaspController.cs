using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspController : MonoBehaviour {

    public GameObject xProjectile;
    public GameObject xProjectileOrigin;

    int iNewDirection;

    Vector3 v3OriginalPosition;
    Vector3 v3TargetPos;
// Move Floats
    public float fMoveSpeed;
    public float fRadius;

    // Fire floats
    public float fFireRate;
    float fFireTimer;
// Move Bools
    private bool bIsAtPosition;
    private bool bHasPosition;

// initial move
    bool bInitialMove;
    bool bInitialPosDecided;
    public bool bDoOnce;

    float fInitialPos;

    bool bFireSound;

    public AudioClip wasp_shoot;
    
    private AudioSource source;

    WaspCollision wasp;

    Animator aAnimator;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        bInitialMove = true;
        bInitialPosDecided = false;
        wasp = GetComponent<WaspCollision>();
        aAnimator = GetComponent<Animator>();
        bHasPosition = true;
        bIsAtPosition = true;
        bFireSound = false;
        bDoOnce = false;
        fFireTimer = fFireRate;
    }
	
	// Update is called once per frame
	void Update () {
        // decides if the wasp should move to a position before it starts it random movement and fire
        if (!bInitialMove) {

            if (fFireTimer > fFireRate){
                fFireTimer = fFireRate;
                
            }

            if(fFireTimer <= 0.3 && !bDoOnce) {
                aAnimator.SetTrigger("tShoot");
                bDoOnce = true;
            }
            // it fires
            if(fFireTimer <= 0.2 && !bFireSound) {
                source.PlayOneShot(wasp_shoot, 1F);
                bFireSound = true;
               
            }

            if (wasp.GetHealth() > 0 && fFireTimer <= 0) {
                GameObject WaspBullet = Instantiate(xProjectile);
                WaspBullet.transform.position = xProjectileOrigin.transform.position;
                fFireTimer = fFireRate;
                bFireSound = false;
                bDoOnce = false;
            }
            if(wasp.GetHealth() >0) {
                MoveWasp();
                SetRandomDirection();
            }     
            

            if (transform.position == v3TargetPos){
                bIsAtPosition = true;
                bHasPosition = false;
            }
            else{
                bIsAtPosition = false;
            }

            fFireTimer -= Time.deltaTime;
    }
    else {
        // gets a random pos to move to it
        if (!bInitialPosDecided){
            fInitialPos = Random.Range(4f, 7f);
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
            v3TargetPos = v3OriginalPosition;
        }
    }
}

    void MoveWasp() {
        // dont have enough enery to write what all of them do. Just move the wasp in different direction
        if (!bIsAtPosition) {
            transform.position = Vector3.MoveTowards(transform.position, v3TargetPos, fMoveSpeed * Time.deltaTime);
        }
    }

    void SetRandomDirection() {
        if (!bHasPosition){
            // OMG Random.insideUnitCircle is a fucking saviour the circle is based on 0,0,0 but is changed to the right pos later by just adding the original pos to the targeted pos
            v3TargetPos = Random.insideUnitCircle * fRadius;
            v3TargetPos.x = v3OriginalPosition.x + v3TargetPos.x;
            v3TargetPos.y = v3OriginalPosition.y + v3TargetPos.y;

            if(v3TargetPos.y >= BeeManager.GetMaxCameraBorder().y - 1) {
                v3TargetPos.y = BeeManager.GetMaxCameraBorder().y - 0.5f;
            }

            if (v3TargetPos.x >= BeeManager.GetMaxCameraBorder().x - 1)
            {
                v3TargetPos.x = BeeManager.GetMaxCameraBorder().x - 1;
            }

            if (v3TargetPos.y <= BeeManager.GetMinCameraBorder().y + 1) {
                v3TargetPos.y = BeeManager.GetMaxCameraBorder().y + 0.5f;
            }

            bHasPosition = true;
        }
        
    }
}
