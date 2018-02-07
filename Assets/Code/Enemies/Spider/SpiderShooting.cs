using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderShooting : MonoBehaviour
{
    public GameObject xSpiderProjectile;
    public GameObject xSpiderProjectileOrigin;

    public GameObject xPlayer;

    public float fFireRate;
    public float fWebSpeed;
    private float fNextShot;

    private Vector3 v3AimDirection;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    v3AimDirection = xPlayer.transform.position;
	    if (Time.time > fNextShot)
	    {
	        GameObject web = Instantiate(xSpiderProjectile);
	        web.transform.position = xSpiderProjectileOrigin.transform.position;
	        float speedDelta = fWebSpeed * Time.deltaTime;
	        fNextShot = Time.time + fFireRate;
	        web.transform.position = Vector3.MoveTowards(transform.position, v3AimDirection, speedDelta);
        }
    }
}
