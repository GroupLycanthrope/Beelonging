﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public GameObject xProjectile;
    public GameObject xProjectileOrigin;


    public int iScoreValue;

    public float fHitPoints;


    public float fFireRate;
    public float fAggroRange;

    public AudioClip spider_dead;
    public AudioClip spider_hit;

    private AudioSource source;

    
    private float fNextShot;
    private float fDeltaX;

    private bool bIsDead;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start ()
    {
        bIsDead = false;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    GameObject xPlayer = GameObject.Find("Player");
        if (xPlayer != null 
            && Time.time > fNextShot 
            && transform.position.x - xPlayer.transform.position.x < fAggroRange
            && !bIsDead)
        {
	        GameObject web = Instantiate(xProjectile);
	        web.transform.position = xProjectileOrigin.transform.position;
	        fNextShot = Time.time + fFireRate;
        }
	    if (transform.position.x < -20)
	    {
	        Destroy(gameObject);
	    }
    }

    void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        if (p_xOtherCollider.gameObject.CompareTag("BeeBullet"))
        {
            if (fHitPoints <= p_xOtherCollider.gameObject.GetComponent<PlayerBullet>().fDamage)
            {
                source.PlayOneShot(spider_dead, 1F);
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                bIsDead = true;
                //TODO: Death animation (maybe with state for dying)
                Destroy(gameObject, 1);
                ScoreManager.iScore += iScoreValue;
            }
            else
            {
                source.PlayOneShot(spider_hit, 1F);
                //TODO: Spider Hit Sound?       
                fHitPoints -= p_xOtherCollider.gameObject.GetComponent<PlayerBullet>().fDamage;
            }
        }
    }
}