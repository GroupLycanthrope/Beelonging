using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {



    public GameObject m_xPlayerBullet;
    public GameObject m_xBulletPosition;

    public float m_fSpeed = 4;
    public float fFireRate;
    private float fNextShot;

    public AudioClip shootsound;
    private AudioSource source;

    public GameObject xGameOverScreen;

    Vector2 m_v2Direction;

    Vector2 m_v2Pos;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("space") && Time.time > fNextShot){
            source.PlayOneShot(shootsound, 1F);
            fNextShot = Time.time + fFireRate;
            GameObject bullet = Instantiate(m_xPlayerBullet);
            bullet.transform.position = m_xBulletPosition.transform.position;
        }


        float fX = Input.GetAxisRaw("Horizontal"); //is this really a parameter? kinda is
        float fY = Input.GetAxisRaw("Vertical"); //   -||-

        m_v2Direction = new Vector2(fX, fY).normalized;
        Move(m_v2Direction);

        if(this.GetComponent<ControlHealth>().getHealth() == 0) {
            xGameOverScreen.SetActive(true);
        }


    }

    void Move(Vector2 p_v2Direction)
    {
        m_v2Pos = transform.position;

        m_v2Pos += p_v2Direction * m_fSpeed * Time.deltaTime;

        if (m_v2Pos.x < -11.55 || m_v2Pos.x > 8.75F || m_v2Pos.y < -5.7F || m_v2Pos.y > 5.7F)
        {
            return;
        }
        else
        {
            transform.position = m_v2Pos;
        }
    }

}
