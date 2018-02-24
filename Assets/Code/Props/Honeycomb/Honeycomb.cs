using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honeycomb : MonoBehaviour
{

    public float fFloatingSpeed;

    public int iHoneyValue;

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
        transform.Translate(-fFloatingSpeed * Time.deltaTime, 0, 0);
    }
}
