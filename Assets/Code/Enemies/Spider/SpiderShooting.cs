using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpiderShooting : MonoBehaviour
{
    public GameObject xSpiderProjectile;
    public GameObject xSpiderProjectileOrigin;

    public float fFireRate;
    private float fNextShot;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Time.time > fNextShot)
	    {
	        GameObject web = Instantiate(xSpiderProjectile);
	        web.transform.position = xSpiderProjectileOrigin.transform.position;
	        fNextShot = Time.time + fFireRate;
        }
    }
}
