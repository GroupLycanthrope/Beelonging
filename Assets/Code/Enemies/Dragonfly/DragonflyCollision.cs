using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyCollision : MonoBehaviour
{
    public float fHitPoints;

    public float fHitFlashSpeed;

    public float fDespawnX;

    public float fDropAcceleration;
    public float fDropMaxVelocity;
    public float fDropMaxY;
    public float fDeadScrollingSpeed;

    private float fDropVelocity;

    public int iScoreValue;

    public Sprite sHoneyDeadSprite;
    public Sprite sBeeCollisionSprite;

    private bool bIsDead = false;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (transform.position.x <= -fDespawnX
	        || transform.position.x >= fDespawnX)
	    {
	        Destroy(gameObject);
	    }

        if (bIsDead)
	    {
	        CollisionDrop();
        }

    }

    void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        if (p_xOtherCollider.gameObject.CompareTag("BeeBullet")
            || p_xOtherCollider.gameObject.CompareTag("Bee"))
        {
            if (fHitPoints <= 1)
            { 
                GetComponent<PolygonCollider2D>().enabled = false;
                //GetComponentInChildren<SpriteRenderer>().enabled = false;
                bIsDead = true;
                ScoreManager.iScore += iScoreValue;
                GetComponent<DragonflyMovement>().enabled = false;
                if (p_xOtherCollider.gameObject.CompareTag("BeeBullet"))
                {
                    GetComponent<SpriteRenderer>().sprite = sHoneyDeadSprite;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = sBeeCollisionSprite;
                }
            }
            else
            {
                TakeDamage(1);
            }
        }
    }

    void TakeDamage(float p_fDamage)
    {
        fHitPoints -= p_fDamage;
        StartCoroutine(SpriteFlasher());
    }

    void CollisionDrop()
    {
        if (transform.position.y > fDropMaxY
            && fDropVelocity > -fDropMaxVelocity)
        {
            fDropVelocity += fDropAcceleration;
        }
        else
        {
            fDropVelocity = 0;
            gameObject.tag = "Dead";
        }
        transform.Translate(-fDeadScrollingSpeed * Time.deltaTime, -fDropVelocity * Time.deltaTime, 0);

    }

    IEnumerator SpriteFlasher()
    {
        for (float f = 1f; f >= 0; f -= fHitFlashSpeed)
        {
            Color temp = GetComponent<SpriteRenderer>().color;
            temp.b = f;
            temp.g = f;
            GetComponent<SpriteRenderer>().color = temp;
            yield return null;
        }
        for (float f = 0f; f <= 1.1; f += fHitFlashSpeed)
        {
            Color temp = GetComponent<SpriteRenderer>().color;
            temp.b = f;
            temp.g = f;
            GetComponent<SpriteRenderer>().color = temp;
            yield return null;
        }
    }
}
