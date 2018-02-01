using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneycombMover : MonoBehaviour
{

    public float fFloatingSpeed;


	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Move();
	}

    void Move()
    {
        transform.Translate(-fFloatingSpeed * Time.fixedDeltaTime, 0, 0);
    }
}
