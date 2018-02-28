using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderProjectileMovement : MonoBehaviour
{
    public float fSpeed;

    public float fDespawnX;

    private Vector3 v3Direction;

    float iDeltaX;
    float iDeltaY;
    public float iHypotenuse;
    public float iDegree;
    public float iRadian;
    GameObject xPlayer;

    // Use this for initialization
    void Start (){

        xPlayer = GameObject.Find("Player");

        iDeltaX = transform.position.x - xPlayer.transform.position.x;
        iDeltaY = transform.position.y - xPlayer.transform.position.y;

        iHypotenuse = Mathf.Sqrt(Mathf.Pow(iDeltaX,2) + Mathf.Pow(iDeltaY,2));

        iRadian = Mathf.Acos((iDeltaX / iHypotenuse));

        iDegree = iRadian * (180 / Mathf.PI);

        if (xPlayer != null)
        {
            if(xPlayer.transform.position.y > transform.position.y){
                transform.Rotate(0, 0, -iDegree);
            }
            else{
                transform.Rotate(0, 0, iDegree);
            }           
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (transform.position.x < -fDespawnX 
	        || transform.position.x > fDespawnX)
	    {
	        Destroy(gameObject);
	    }
        transform.position = Vector3.MoveTowards(transform.position, xPlayer.transform.position, fSpeed * Time.deltaTime);
        //transform.Translate(v3Direction * fSpeed * Time.deltaTime);
	}
}
