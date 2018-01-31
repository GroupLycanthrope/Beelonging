using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeManager : MonoBehaviour {


    public GameObject m_xPlayerBullet;
    public GameObject m_xBulletPosition;
    public GameObject m_xPlayer;
    public GameObject xGameOverScreen;
    public GameObject xScoreText;

    public AudioClip shootsound;
    private AudioSource source;

    public float m_fSpeed;

    public float fFireRate;

    private float fNextShot;

    private int iScore;

    Vector2 m_v2Pos;

    Vector2 m_v2Direction;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        m_fSpeed = 4;
    }
	
	// Update is called once per frame
	void Update () {
	    if (m_xPlayer != null)
	    {
	        if (Input.GetKey("space") && Time.time > fNextShot)
	        {
	            source.PlayOneShot(shootsound, 1F);
                fNextShot = Time.time + fFireRate;
	            GameObject bullet = Instantiate(m_xPlayerBullet);
	            bullet.transform.position = m_xBulletPosition.transform.position;
	        }

	        float p_fX = Input.GetAxisRaw("Horizontal"); //is this really a parameter?
	        float p_fY = Input.GetAxisRaw("Vertical"); //   -||-

	        m_v2Direction = new Vector2(p_fX, p_fY).normalized;
	        Move(m_v2Direction);
	    }
	    else 
	    {
	        xGameOverScreen.SetActive(true);
	    }
    }

    void Move(Vector2 p_v2Direction){
        m_v2Pos = m_xPlayer.transform.position;

        m_v2Pos += p_v2Direction * m_fSpeed * Time.deltaTime;

        if(m_v2Pos.x < -11.55 || m_v2Pos.x > 8.75F || m_v2Pos.y < -5.7F || m_v2Pos.y > 5.7F){
            return;
        }else {
            m_xPlayer.transform.position = m_v2Pos;
        }        
    }

    void AddScore(int p_iScoreToAdd)
    {
        iScore += p_iScoreToAdd;
    }

    void UpdateScore()
    {
    }
}