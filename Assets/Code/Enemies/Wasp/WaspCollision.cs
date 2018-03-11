using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspCollision : MonoBehaviour {

    public float fHitPoints;
    public float fHitFlashSpeed;
    public float fDamageFireRateMultiplier;
    public float fDropAcceleration;
    public float fDropMaxVelocity;
    public float fDropMaxY;
    public float fDeadScrollingSpeed;

    private float fDropVelocity;

    public int iScoreValue;

    private bool bIsDead = false;
    bool bHoneyed = false;
    bool bCollided = false;


    public AudioClip wasp_hurt;
    public AudioClip wasp_death;
    private AudioSource source;

    Animator aAnim;

    public Sprite sHoneyDeadSprite;
    public Sprite sBeeCollisionSprite;
   
    WaspController wasp;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        aAnim = gameObject.GetComponent<Animator>();
        wasp = gameObject.GetComponent<WaspController>();
    }

    void Update () {
        if (transform.position.x < -20){
            Destroy(gameObject);
        }

        if (fHitPoints <= 0 && !bIsDead){
            //GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            bIsDead = true;
            aAnim.enabled = false;

            if (bIsDead && bHoneyed){
                GetComponent<SpriteRenderer>().sprite = sHoneyDeadSprite;
            }

            if (bIsDead && bCollided){
                GetComponent<SpriteRenderer>().sprite = sBeeCollisionSprite;
            }

            Destroy(gameObject, 1);
            source.PlayOneShot(wasp_death, 1F);
            ScoreManager.iScore += iScoreValue;
           
        }
        if (bIsDead) {
            CollisionDrop();
        }
    }

    void OnCollisionEnter2D(Collision2D p_xOtherCollider){
        if (p_xOtherCollider.gameObject.CompareTag("BeeBullet")){
            TakeDamage(p_xOtherCollider.gameObject.GetComponent<PlayerBullet>().fDamage);
            source.PlayOneShot(wasp_hurt, 1F);
            if(fHitPoints <= 0) {
                bHoneyed = true;
            }
            
        }

        if (p_xOtherCollider.gameObject.CompareTag("Bee")){
            TakeDamage(1);
            source.PlayOneShot(wasp_hurt, 1F);
            if (fHitPoints <= 0){
                bCollided = true;
            }
        }
    }

    void TakeDamage(float p_fDamage){
        fHitPoints -= p_fDamage;
        wasp.fFireRate *= fDamageFireRateMultiplier;
        StartCoroutine(SpriteFlasher());
    }
    
    public float GetHealth() {
        return fHitPoints;
    }

    void CollisionDrop(){
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

    IEnumerator SpriteFlasher(){
        for (float f = 1f; f >= 0; f -= fHitFlashSpeed){
            Color temp = GetComponent<SpriteRenderer>().color;
            temp.b = f;
            temp.g = f;
            GetComponent<SpriteRenderer>().color = temp;
            yield return null;
        }
        for (float f = 0f; f <= 1; f += fHitFlashSpeed){
            Color temp = GetComponent<SpriteRenderer>().color;
            temp.b = f;
            temp.g = f;
            GetComponent<SpriteRenderer>().color = temp;
            yield return null;
        }
    }

}
