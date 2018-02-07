﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyMovement : MonoBehaviour
{
    public float fZoomSpeed;

    public Vector2 v2RandomMinInterval;
    public Vector2 v2RandomMaxInterval;

    public float fZoomCooldown;

    private float fNextZoom;

    private Vector3 v3ZoomDirection;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    //if (transform.position.x < -20)
	    //{
     //       Destroy(gameObject);
	    //}
		//transform.Translate(-fFlyingSpeed * Time.deltaTime, 0, 0);
	    if (Time.time > fNextZoom)
	    {
	        fNextZoom = Time.time + fZoomCooldown;
	        float fRandomX = Random.Range(v2RandomMinInterval.x, v2RandomMaxInterval.x);
	        float fRandomY = Random.Range(v2RandomMinInterval.y, v2RandomMaxInterval.y);
	        v3ZoomDirection.x = transform.position.x - fRandomX;
	        v3ZoomDirection.y = transform.position.y - fRandomY;
	        v3ZoomDirection.Normalize();
	    }

	    if (transform.position.x > v3ZoomDirection.x)
	    {
            transform.Translate(v3ZoomDirection * fZoomSpeed * Time.deltaTime);
	    }
	}
}