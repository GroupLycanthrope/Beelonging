﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float fFireRate;
    private float fNextShot;

    public float fAcceleration;
    public float fDeceleration;
    public float fMaxVelocity;
    private float fCurrentMaxVelocity;
    private float fShootingMaxVelocity;
    private float fVelocityX;
    private float fVelocityY;

    public Vector2 v2PlayerBoundariesMin;
    public Vector2 v2PlayerBoundariesMax;

    public float fShotSlowDown;

    public AudioClip shootsound;
    public AudioClip changeform;
    private AudioSource source;

    private Animator aAnimator;

    [HideInInspector]
    public Vector3 v3Direction;

    private BeeCollision sBeeCollision;

    private GameObject goBulletStartPosition;
    private GameObject goPauseMenu;
    private void Awake()
    {
        fNextShot = Time.time;
        goBulletStartPosition = transform.GetChild(0).gameObject;
        sBeeCollision = GetComponent<BeeCollision>();
        source = GetComponent<AudioSource>();
        aAnimator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start ()
    {
        fCurrentMaxVelocity = fMaxVelocity;
        fShootingMaxVelocity = fMaxVelocity * fShotSlowDown;
    }
	
	// Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey("space")
            && !sBeeCollision.bIsDead
            && Time.time > fNextShot)
        {
            if (BeeManager.bFormationActive)
            {
                foreach (GameObject Bee in BeeManager.aSwarm)
                {
                    Bee.SendMessage("Shoot");
                    Shoot();
                }
            }
            else
            {
                Shoot();
            }
        }

        if (Input.GetKeyUp("space"))
        {
            ResetVelocity();
        }

        if (Input.GetKeyDown("x")
        && BeeManager.fHoneyCount > 0
        && Time.timeScale > 0)
        {
            source.PlayOneShot(changeform, 1F);
            BeeManager.bFormationActive = true;
        }
        else if (Input.GetKeyUp("x")
        || BeeManager.fHoneyCount <= 0)
        {
            BeeManager.bFormationActive = false;
            BeeManager.UnOccupyPositions();
        }

        if (Input.GetKey(KeyCode.UpArrow) && fVelocityY < fCurrentMaxVelocity)
        {
            fVelocityY += fAcceleration;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && fVelocityY > -fCurrentMaxVelocity)
        {
            fVelocityY -= fAcceleration;
        }
        else
        {
            if (fVelocityY > 0)
            {
                fVelocityY -= fDeceleration;
            }
            else if (fVelocityY < 0)
            {
                fVelocityY += fDeceleration;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) && fVelocityX < fCurrentMaxVelocity)
        { 
            fVelocityX += fAcceleration;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && fVelocityX > -fCurrentMaxVelocity)
        {
            fVelocityX -= fAcceleration;
        }
        else
        {
             if (fVelocityX > 0)
             {
                 fVelocityX -= fDeceleration;
             }
             else if (fVelocityX < 0)
             {
                 fVelocityX += fDeceleration;
             }
        }
    
        Vector3 v3NewPosition;
        v3NewPosition.x = Mathf.Clamp(transform.position.x + fVelocityX * Time.deltaTime, v2PlayerBoundariesMin.x, v2PlayerBoundariesMax.x);
        v3NewPosition.y = Mathf.Clamp(transform.position.y + fVelocityY * Time.deltaTime, v2PlayerBoundariesMin.y, v2PlayerBoundariesMax.y);
        v3NewPosition.z = -2;
        transform.position = v3NewPosition;
        v3Direction.x = fVelocityX;
        v3Direction.y = fVelocityY;
        v3Direction.Normalize();
        
    }
    void ResetVelocity()
    {
        fCurrentMaxVelocity = fMaxVelocity;
    }

    void Shoot()
    {     
         aAnimator.SetTrigger("tShot");
         source.PlayOneShot(shootsound, 1F);
         fNextShot = Time.time + fFireRate;
         GameObject newBullet = Instantiate(Resources.Load("BeeStuff/Player/PlayerBullet")) as GameObject;
         newBullet.transform.position = goBulletStartPosition.transform.position; //Gets the first child of this gameobject and returns the position of its tranform (I know it looks weird but this way we don't have to link the bulletposition)
         fVelocityY *= fShotSlowDown;
         fVelocityX *= fShotSlowDown;
         fCurrentMaxVelocity = fShootingMaxVelocity;
    }
}