﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderProjectileCollision : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        if (p_xOtherCollider.gameObject.CompareTag("Bee"))
        {
            Destroy(gameObject);
        }
    }
}
