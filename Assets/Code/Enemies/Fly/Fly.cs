using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public GameObject xFly;

    public float iHealth;

    public float fFlyingSpeed;

    public int iScoreValue;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }

    void Move()
    {
        transform.Translate(-fFlyingSpeed * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        if (p_xOtherCollider.gameObject.CompareTag("BeeBullet"))
        {
            //TODO: Death animation (maybe with state for dying)
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        ScoreManager.iScore += iScoreValue;
    }
}