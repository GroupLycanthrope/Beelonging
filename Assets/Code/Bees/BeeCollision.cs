using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeCollision : MonoBehaviour
{

    public AudioClip deathsound;
    public AudioClip honeycomb_pickup;
    public AudioClip honeycomb_activate;

    private AudioSource source;

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

    }

    void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        //if (!BeeManager.bIsInvincible)
        //{
            if (p_xOtherCollider.gameObject.CompareTag("Enemy") ||
                p_xOtherCollider.gameObject.CompareTag("EnemyProjectile"))
            {
                source.PlayOneShot(deathsound, 1F);
                //TODO: Death Animation
                BeeManager.KillBeell(gameObject);
            }
        //}

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
}
