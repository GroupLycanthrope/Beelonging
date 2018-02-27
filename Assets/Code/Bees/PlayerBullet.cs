using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float fSpeed;

    public float fDamage;

    Vector2 v2Position;

    private Animator aAnimator;

    Vector3 v3GrowRate;
    public float fBulletGrowRate;
    public float fBulletSize;
    
    // Use this for initialization
    void Start (){
        v3GrowRate.x = fBulletGrowRate;
        v3GrowRate.y = fBulletGrowRate;
        aAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        v2Position = transform.position;

        v2Position = new Vector2(v2Position.x + fSpeed * Time.deltaTime, v2Position.y);

        if(transform.localScale.x < fBulletSize) {
            transform.localScale += v3GrowRate;
        }

        transform.position = v2Position;

        if(v2Position.x > 9) {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        if (p_xOtherCollider.gameObject.CompareTag("Enemy"))
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            fSpeed = 0;
            aAnimator.SetBool("bHasHit", true);
            Invoke("StopAnimation", 0.25f);    
            //TODO: Add splash of bullet exploding before dissapearing?
            Destroy(gameObject, 1);
        }
    }

    void StopAnimation()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    
}
