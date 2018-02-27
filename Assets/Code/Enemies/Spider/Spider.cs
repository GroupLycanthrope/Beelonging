using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public GameObject xProjectile;
    public GameObject xProjectileOrigin;


    public int iScoreValue;

    public float fHitPoints;


    public float fFireRate;
    public float fAggroRange;
    public float fDespawnX;

    public float fHitFlashSpeed;

    public AudioClip spider_dead;
    public AudioClip spider_hit;
    public AudioClip spider_shoot;

    private AudioSource source;

    
    private float fNextShot;
    private float fDeltaX;

    private bool bIsDead;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start ()
    {
        bIsDead = false;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    GameObject xPlayer = GameObject.Find("Player");
        if (xPlayer != null 
            && Time.time > fNextShot 
            && transform.position.x - xPlayer.transform.position.x < fAggroRange
            && !bIsDead)
        {
            source.PlayOneShot(spider_shoot, 1F);
            GameObject web = Instantiate(xProjectile);
	        web.transform.position = xProjectileOrigin.transform.position;
	        fNextShot = Time.time + fFireRate;
        }
	    if (transform.position.x <= fDespawnX)
	    {
	        Destroy(gameObject);
	    }

	    if (fHitPoints <= 0 && !bIsDead)
	    {
	        source.PlayOneShot(spider_dead, 1F);
	        GetComponent<SpriteRenderer>().enabled = false;
	        GetComponent<BoxCollider2D>().enabled = false;
	        GetComponentInChildren<SpriteRenderer>().enabled = false;
	        bIsDead = true;
	        //TODO: Death animation (maybe with state for dying)
	        Destroy(gameObject, 1);
	        ScoreManager.iScore += iScoreValue;
        }
    }

    void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        if (p_xOtherCollider.gameObject.CompareTag("BeeBullet"))
        {
            TakeDamage(p_xOtherCollider.gameObject.GetComponent<PlayerBullet>().fDamage);
        }

        if (p_xOtherCollider.gameObject.CompareTag("Bee"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(float p_fDamage)
    {
        source.PlayOneShot(spider_hit, 1F);
        fHitPoints -= p_fDamage;
        StartCoroutine(SpriteFlasher());
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

    //IEnumerator Fade()
    //{
    //    for (float f = 1f; f >= 0; f -= 0.1f)
    //    {
    //        Color c = renderer.material.color;
    //        c.a = f;
    //        renderer.material.color = c;
    //        yield return null;
    //    }
    //}
}