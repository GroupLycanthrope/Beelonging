﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{ 
    public float fFlyingSpeed;

    public int iScoreValue;

    public AudioClip fly_dead;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        GetComponent<Animator>().SetFloat("fAnimationOffset", Random.Range(0, 1));
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        transform.Translate(-fFlyingSpeed * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        if (p_xOtherCollider.gameObject.CompareTag("BeeBullet") || p_xOtherCollider.gameObject.CompareTag("Bee"))
        {
            source.PlayOneShot(fly_dead, 1F);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            //TODO: Death animation (maybe with state for dying)
            Destroy(gameObject, 1);
            ScoreManager.iScore += iScoreValue;

        }
    }
}