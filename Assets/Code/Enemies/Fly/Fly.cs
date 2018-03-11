using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Playables;

public class Fly : MonoBehaviour
{ 
    public float fFlyingSpeed;

    public Vector2 v2FlingingOffSpeed;
    public float fFlingingOffRotationSpeed;

    public float fDropAcceleration;
    public float fDropMaxVelocity;
    public float fDropMaxY;
    public float fDeadScrollingSpeed;
    public float fTrailClock;
    private float fDropVelocity;
    private float fTrailTimer;

    public int iScoreValue;

    public AudioClip fly_dead;

    private AudioSource source;

    private SpriteRenderer sRenderer;
    private PolygonCollider2D pcCollider;
    private Animator aAnimator;

    public Sprite sHoneyDeadSprite;
    public Sprite sBeeCollisionSprite;
    public Material mTrailMaterial;

    private bool bHoneyed = false;
    private bool bCollided = false;
    private bool bActiveTrail;
    private bool bDoOnce;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        aAnimator = GetComponent<Animator>();
        aAnimator.SetFloat("fAnimationOffset", Random.value);
        sRenderer = GetComponent<SpriteRenderer>();
        pcCollider = GetComponent<PolygonCollider2D>();
    }

    // Use this for initialization
    void Start()
    {
        bActiveTrail = false;
        bDoOnce = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (bActiveTrail && bDoOnce){
            gameObject.AddComponent<TrailRenderer>();
            gameObject.GetComponent<TrailRenderer>().material = mTrailMaterial;
            gameObject.GetComponent<TrailRenderer>().widthMultiplier = 0.1f;
            gameObject.GetComponent<TrailRenderer>().time = 0.5f;
            bDoOnce = false;
        }
            
       
        
        if (bHoneyed)
        {
            FlingOff();
            bActiveTrail = true;
            bDoOnce = true;
        }
        else if (bCollided)
        {
            CollisionDrop();
        }
        else
        {
            Move();
        }

        if (transform.position.x < BeeManager.GetMinCameraBorder().x - 1 || transform.position.y < BeeManager.GetMinCameraBorder().y -3){
            Destroy(gameObject);
        }
    }

    void Move()
    {
        transform.Translate(-fFlyingSpeed * Time.deltaTime, 0, 0);
    }

    void FlingOff()
    {
        transform.Rotate(0, 0, -fFlingingOffRotationSpeed * Time.deltaTime);
        Vector3 tmp = transform.position;
        tmp.x += v2FlingingOffSpeed.x * Time.deltaTime;
        tmp.y -= v2FlingingOffSpeed.y * Time.deltaTime;
        transform.position = tmp;
        transform.Translate(-fFlyingSpeed * Time.deltaTime, 0, 0);

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

    void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        if (p_xOtherCollider.gameObject.CompareTag("BeeBullet") 
            || p_xOtherCollider.gameObject.CompareTag("Bee"))
        {
            aAnimator.enabled = false;
            source.PlayOneShot(fly_dead, 1F);
            pcCollider.enabled = false;
            ScoreManager.iScore += iScoreValue;
            if (p_xOtherCollider.gameObject.CompareTag("BeeBullet"))
            {
                sRenderer.sprite = sHoneyDeadSprite;
                bHoneyed = true;
                //GetComponent<ParticleSystem>().Play();
            }
            else
            {
                sRenderer.sprite = sBeeCollisionSprite;
                bCollided = true;
            }

        }
    }
}