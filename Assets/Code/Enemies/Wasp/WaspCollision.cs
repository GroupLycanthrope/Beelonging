using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspCollision : MonoBehaviour {

    public float fHitPoints;

    public float fHitFlashSpeed;

    public int iScoreValue;

    public float fDamageFireRateMultiplier;

    private bool bIsDead = false;

    WaspController wasp;

    private void Start()
    {
        wasp = gameObject.GetComponent<WaspController>();
    }

    void Update () {
        if (transform.position.x < -20){
            Destroy(gameObject);
        }

        if (fHitPoints <= 0 && !bIsDead){
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            Destroy(gameObject, 1);
            bIsDead = true;
            ScoreManager.iScore += iScoreValue;
        }
    }

    void OnCollisionEnter2D(Collision2D p_xOtherCollider){
        if (p_xOtherCollider.gameObject.CompareTag("BeeBullet")){
            TakeDamage(p_xOtherCollider.gameObject.GetComponent<PlayerBullet>().fDamage);
        }

        if (p_xOtherCollider.gameObject.CompareTag("Bee")){
            TakeDamage(1);
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
