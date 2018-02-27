using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public GameObject xProjectile;
    public GameObject xProjectileOrigin;

    public int iScoreValue;

    private int iDirection;
    private int iRandomNumber;

    public float fMoveUpAndDownSpeed;
    public float fHitPoints;
    public float fFireRate;
    public float fAggroRange;
    public float fDespawnX;
    public float fHitFlashSpeed;
    public float fStandStillBeforeFire;

    private  float fNextShot;
    private float fDeltaX;
    
    public AudioClip spider_dead;
    public AudioClip spider_hit;
    public AudioClip spider_shoot;

    private AudioSource source;

    private bool bIsDead;
    private bool bWantToFire;
    private bool bWantToMove;
    private bool bHasDestination;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start (){
        fNextShot = fFireRate;
        bWantToMove = false;
        bIsDead = false;
    }
	
	// Update is called once per frame
	void Update (){

        fNextShot -= Time.deltaTime;
        if (bWantToFire) {
            GameObject xPlayer = GameObject.Find("Player");
            if (xPlayer != null
                && fNextShot <= 0
                && transform.position.x - xPlayer.transform.position.x < fAggroRange
                && !bIsDead){
                source.PlayOneShot(spider_shoot, 1F);
                GameObject web = Instantiate(xProjectile);
                web.transform.position = xProjectileOrigin.transform.position;
                fNextShot = fFireRate;
                bWantToMove = true;
            }
        }
        else {
            Move();
        }


	    if (transform.position.x <= fDespawnX){
	        Destroy(gameObject);
	    }

        if(fNextShot <= fStandStillBeforeFire) {
            bWantToFire = true;
        }
        else{
            bWantToFire = false;
        }

	    if (fHitPoints <= 0 && !bIsDead){
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

    void Move() {
        
        if (bWantToMove) {
            iRandomNumber = Random.Range(0, 19);
            bWantToMove = false;
            bHasDestination = true;
        }
        if(iRandomNumber <= 9 && bHasDestination) {
            iDirection = 0;
            bHasDestination = false;
        }
        if (iRandomNumber >= 10 && bHasDestination){
            iDirection = 1;
            bHasDestination = false;
        }

        if (transform.position.y <= 0.2 && !bWantToMove){
            iDirection = 0;
        }
        if (transform.position.y >= 4.2 && !bWantToMove){
            iDirection = 1;
        }

        if (iDirection == 0 && !(transform.position.y >= 4.2)) {
            transform.Translate(Vector2.up * fMoveUpAndDownSpeed * Time.deltaTime);
        }
        if(iDirection == 1 && !(transform.position.y <= 0.2)) {
            transform.Translate(Vector2.down * fMoveUpAndDownSpeed * Time.deltaTime);
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
        for (float f = 0f; f <= 1; f += fHitFlashSpeed)
        {
            Color temp = GetComponent<SpriteRenderer>().color;
            temp.b = f;
            temp.g = f;
            GetComponent<SpriteRenderer>().color = temp;
            yield return null;
        }
    }
}