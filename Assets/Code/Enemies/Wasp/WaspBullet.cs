using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspBullet : MonoBehaviour {

    public float fSpeed;


    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < -20 || transform.position.x > 20){
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
