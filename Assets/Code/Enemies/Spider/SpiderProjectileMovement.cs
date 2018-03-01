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
        v3Direction = xPlayer.transform.position;

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
        if (transform.position.x < BeeManager.GetMinCameraBorder().x - 1 || transform.position.x > BeeManager.GetMaxCameraBorder().x + 1){
            Destroy(gameObject);
        }
        if (transform.position.y < BeeManager.GetMinCameraBorder().y - 1 || transform.position.y > BeeManager.GetMaxCameraBorder().y + 1)
        {
            Destroy(gameObject);
        }

        transform.Translate(-1 * fSpeed *Time.deltaTime,0,0);
	}
}
