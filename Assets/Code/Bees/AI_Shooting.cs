using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Shooting : MonoBehaviour {

    public GameObject goBulletStartPos;

    public GameObject goBullet;

    Ray2D Sight;

    float rayDist = 10;

    Vector3 dist;
   
    void Start () {
        dist.x = rayDist;
    }
	
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(goBulletStartPos.transform.position, Vector2.right, rayDist);
        Debug.DrawRay(goBulletStartPos.transform.position, dist, Color.red);
        if(hit.collider != null) {

            if (hit.collider.gameObject.name == "Player"){
                Debug.Log(hit.collider.name);
            }
        }
    }
}
