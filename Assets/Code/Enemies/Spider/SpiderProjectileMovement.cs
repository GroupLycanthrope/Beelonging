using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderProjectileMovement : MonoBehaviour
{
    public float fSpeed;
   
    private Vector3 v3Direction;

	// Use this for initialization
	void Start ()
	{
        var xPlayer = GameObject.Find("Player");
        if (xPlayer != null)
        { 
	        v3Direction = xPlayer.transform.position - transform.position;
            v3Direction.Normalize();
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (transform.position.x < -20 || transform.position.x > 20)
	    {
	        Destroy(gameObject);
	    }
        transform.Translate(v3Direction * fSpeed * Time.deltaTime);
	}
}
