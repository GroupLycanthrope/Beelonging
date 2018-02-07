using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyCollision : MonoBehaviour
{
    public float fHitPoints;

    public int iScoreValue;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

    void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        if (p_xOtherCollider.gameObject.CompareTag("BeeBullet"))
        {
            if (fHitPoints <= p_xOtherCollider.gameObject.GetComponent<PlayerBullet>().fDamage)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<PolygonCollider2D>().enabled = false;
                Destroy(gameObject, 1);
                ScoreManager.iScore += iScoreValue;
            }
            else
            {
                fHitPoints -= p_xOtherCollider.gameObject.GetComponent<PlayerBullet>().fDamage;
            }
        }
    }
}
