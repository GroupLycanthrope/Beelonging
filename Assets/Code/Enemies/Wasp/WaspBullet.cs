using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspBullet : MonoBehaviour {

    public float fSpeed;



    Vector3 v3GrowRate;
    public float fBulletGrowRate;
    public float fBulletSize;

    // Use this for initialization
    void Start () {
        v3GrowRate.x = fBulletGrowRate;
        v3GrowRate.y = fBulletGrowRate;
    }
	
	// Update is called once per frame
	void Update () {

        if (transform.localScale.x < fBulletSize){
            transform.localScale += v3GrowRate;
        }

        if (transform.position.x < BeeManager.GetMinCameraBorder().x - 1){
            Destroy(gameObject);
        }
        transform.Translate(Vector2.left * fSpeed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        if (p_xOtherCollider.gameObject.CompareTag("Bee"))
        {
            Destroy(gameObject);
        }
    }
}
