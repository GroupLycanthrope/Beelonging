using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeCollision : MonoBehaviour
{
    public float fDespawnX;

    public AudioClip deathsound;
    public AudioClip honeycomb_pickup;
    public AudioClip honeycomb_activate;

    private AudioSource source;

    public float fDropAcceleration;
    public float fDropMaxVelocity;
    public float fDropMaxY;
    public float fDeadScrollingSpeed;

    private float fDropVelocity;

    public Sprite sCollisionSprite;
    public Sprite sWebbedSprite;

    [HideInInspector]
    public bool bIsDead;
    
    public float fHitFlashSpeed;

    private void Awake()
    {
        bIsDead = false;
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -fDespawnX
            || transform.position.x > fDespawnX)
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
        if (p_xOtherCollider.gameObject.CompareTag("Enemy") 
            ||p_xOtherCollider.gameObject.CompareTag("Web")
            ||p_xOtherCollider.gameObject.CompareTag("Stinger"))
        {
            GetComponent<Animator>().enabled = false;
            source.PlayOneShot(deathsound, 1F);
            GetComponent<CapsuleCollider2D>().enabled = false;
            bIsDead = true;
            BeeManager.KillBeell(gameObject);
            if (p_xOtherCollider.gameObject.CompareTag("Web"))
            {
                GetComponent<SpriteRenderer>().sprite = sWebbedSprite;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = sCollisionSprite;
            }
            if (gameObject.name == "Player")
            {
                BeeManager.bPlayerDead = true;
                GetComponent<PlayerController>().enabled = false;
            }
            else
            {
                GetComponent<AIMovement>().enabled = false;
                GetComponent<AIShooting>().enabled = false;
            }

            gameObject.name = "DeadBee";
        }

        if (p_xOtherCollider.gameObject.CompareTag("HoneycombPickUp")) {

            source.PlayOneShot(honeycomb_pickup, 1F);
        }
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

    void StartSpriteFlasher()
    {
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
}
