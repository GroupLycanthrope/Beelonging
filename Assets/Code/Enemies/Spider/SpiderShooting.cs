using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpiderShooting : MonoBehaviour
{
    public GameObject xProjectile;
    public GameObject xProjectileOrigin;

    public float fFireRate;
    public float fAggroRange;
    private float fNextShot;
    private float fDeltaX;

    // Use this for initialization
    void Start ()
	{

    }
	
	// Update is called once per frame
	void Update ()
	{
	    var xPlayer = GameObject.Find("Player");
        if (xPlayer != null && Time.time > fNextShot && transform.position.x - xPlayer.transform.position.x < fAggroRange)
	    {
	        GameObject web = Instantiate(xProjectile);
	        web.transform.position = xProjectileOrigin.transform.position;
	        fNextShot = Time.time + fFireRate;
        }
    }
}