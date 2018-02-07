using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Shooting : MonoBehaviour {

    //Bool
    private bool PlayerInWay;
    //Float
    public float fFireRate;
    public float fNextShot;
    //Sound
    public AudioClip acShootsound;
    private AudioSource asSource;
    //GameObj
    public GameObject goBulletStartPos;
    public GameObject goBullet;
    //Raycasting
    float rayDist = 10;
    Vector3 dist;
   
    void Start () {
        dist.x = rayDist;
    }
	
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(goBulletStartPos.transform.position, Vector2.right, rayDist);
        Debug.DrawRay(goBulletStartPos.transform.position, dist, Color.red);
        if (hit == true) {
           if(hit.collider.gameObject.name == "Player") {
                PlayerInWay = true;
           }else {
                PlayerInWay = false;
            }
        }
        else {
            if(Time.time > fNextShot && PlayerInWay == false && GetComponentInChildren<Find_Move>().BeeCallerFormation == true) {
                fNextShot = Time.time + fFireRate / 5;
                GameObject bullet = Instantiate(goBullet);
                bullet.transform.position = goBulletStartPos.transform.position;
            }
            else if (Time.time > fNextShot && PlayerInWay == false){
                fNextShot = Time.time + fFireRate;
                GameObject bullet = Instantiate(goBullet);
                bullet.transform.position = goBulletStartPos.transform.position;
            }
        }
    }

    void stopShoot() {

    }
}
