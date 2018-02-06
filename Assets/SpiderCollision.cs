﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCollision : MonoBehaviour
{
    public int iScoreValue;

    public float fHitPoints;
	
    
    // Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void TakeDamage(float p_fDamage)
    {
        
    }

    void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        if (p_xOtherCollider.gameObject.CompareTag("BeeBullet"))
        {
            if (fHitPoints <= p_xOtherCollider.gameObject.GetComponent<PlayerBullet>().fDamage)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                //TODO: Death animation (maybe with state for dying)
                Destroy(gameObject, 1);
                ScoreManager.iScore += iScoreValue;
            }
            else
            {
                //TODO: Spider Hit Sound?       
                fHitPoints -= p_xOtherCollider.gameObject.GetComponent<PlayerBullet>().fDamage;
            }
        }
    }
}
