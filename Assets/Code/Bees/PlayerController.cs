using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public Vector2 v2Min;
    public Vector2 v2Max;
}

public class PlayerController : MonoBehaviour {
    public GameObject m_xPlayerBullet;
    public GameObject m_xBulletPosition;
    //private Rigidbody2D xRigidbody2D;

    //public float m_fSpeed;
    public float fFireRate;
    private float fNextShot;

    public float fAcceleration;
    public float fDeceleration;
    public float fMaxVelocity;
    private float fCurrentMaxVelocity;
    private float fShootingMaxVelocity;
    private float fVelocityX;
    private float fVelocityY;

    public float fShotSlowDown;
	public bool bShootFired;
    public AudioClip shootsound;
    private AudioSource source;

    Vector2 m_v2Direction;

    Vector2 m_v2Pos;

    //private Vector3 v3PlayerBounds;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        //v3PlayerBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        fCurrentMaxVelocity = fMaxVelocity;
        fShootingMaxVelocity = fMaxVelocity * fShotSlowDown;
       // GetComponent<Animator>().SetFloat("fAnimationOffset", Random.Range(0, 1));
        //xRigidbody2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("space") && Time.time > fNextShot){
            GetComponent<Animator>().SetTrigger("tShot");
            source.PlayOneShot(shootsound, 1F);
            fNextShot = Time.time + fFireRate;
            GameObject bullet = Instantiate(m_xPlayerBullet);
            bullet.transform.position = m_xBulletPosition.transform.position;
            fVelocityY *= fShotSlowDown;
            fVelocityX *= fShotSlowDown;
            fCurrentMaxVelocity = fShootingMaxVelocity;
			            bShootFired = true;
        }
		else {
            bShootFired = false;
        }

	    if (Input.GetKeyUp("space"))
	    {
	        ResetVelocity();
	    }

        if (Input.GetKey(KeyCode.UpArrow) && fVelocityY < fCurrentMaxVelocity)
        {
            fVelocityY += fAcceleration;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && fVelocityY > -fCurrentMaxVelocity)
        {
            fVelocityY -= fAcceleration;
        }
        else
        {
            if (fVelocityY > 0)
            {
                fVelocityY -= fDeceleration;
            }
            else if (fVelocityY < 0)
            {
                fVelocityY += fDeceleration;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) && fVelocityX < fCurrentMaxVelocity)
        {
            fVelocityX += fAcceleration;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && fVelocityX > -fCurrentMaxVelocity)
        {
            fVelocityX -= fAcceleration;
        }
        else
        {
            {
                if (fVelocityX > 0)
                {
                    fVelocityX -= fDeceleration;
                }
                else if (fVelocityX < 0)
                {
                    fVelocityX += fDeceleration;
                }
            }
        }

        Vector3 v3NewPosition;
        v3NewPosition.x = Mathf.Clamp(transform.position.x + fVelocityX * Time.deltaTime, -11.55f, 8.75f);
	    v3NewPosition.y = Mathf.Clamp(transform.position.y + fVelocityY * Time.deltaTime, -5.5f, 5.5f);
	    v3NewPosition.z = 0;
	    transform.position = v3NewPosition;

        //transform.Translate(Mathf.Clamp(fVelocityX * Time.deltaTime, -11.55f, 8.75f), Mathf.Clamp(fVelocityY * Time.deltaTime, -5.7f, 5.7f), 0);

	    //float fX = Input.GetAxisRaw("Horizontal");
	    //float fY = Input.GetAxisRaw("Vertical");

	    //m_v2Direction = new Vector2(fX, fY).normalized;
	    //xRigidbody2D.AddForce(m_v2Direction);
	    //Move(m_v2Direction);
	}

    void ResetVelocity()
    {
        fCurrentMaxVelocity = fMaxVelocity;
    }
    //void Move(Vector2 p_v2Direction)
    //{
    //    m_v2Pos = transform.position;

    //    m_v2Pos += p_v2Direction * m_fSpeed * Time.deltaTime;

    //    if (m_v2Pos.x < -11.55 || m_v2Pos.x > 8.75F || m_v2Pos.y < -5.7F || m_v2Pos.y > 5.7F)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        xRigidbody2D.velocity = transform.forward * m_fSpeed;
    //        transform.position = m_v2Pos;
    //    }
    //}
}