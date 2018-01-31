using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    float fSpeed;
    public AudioClip hitsound;
    private AudioSource source;

    Vector2 v2Position;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        fSpeed = 8;

    }
	
	// Update is called once per frame
	void Update () {
        v2Position = transform.position;

        v2Position = new Vector2(v2Position.x + fSpeed * Time.deltaTime, v2Position.y);

        //if(m_v2Position.x < 0) {
        //    m_v2Position.x += m_v2Position.x * -m_fSpeed * Time.deltaTime;
        //}
        //else {
        //    m_v2Position.x += m_v2Position.x * m_fSpeed * Time.deltaTime;
        //}

        

        transform.position = v2Position;

        if(v2Position.x > 9) {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        if (p_xOtherCollider.gameObject.CompareTag("Enemy"))
        {
            source.PlayOneShot(hitsound, 1F);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            //TODO: Add splash of bullet exploding before dissapearing?
            Destroy(gameObject, 1);
        }
    }
    
}
