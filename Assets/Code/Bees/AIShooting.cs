using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    public float fFireRate;
    private float fNextShot;

    public AudioClip acShotSound;
    private AudioSource asSource;

    private float fRayDistance = 10;

    private Vector3 v3DebugRay;

    private GameObject goBulletStartPosition;

    private BeeCollision sBeeCollision;
    private Animator aAnimator;
    
    void Awake()
    {
        sBeeCollision = GetComponent<BeeCollision>();
        aAnimator = GetComponent<Animator>();
        aAnimator.SetFloat("fAnimationOffset", Random.value);
        asSource = GetComponent<AudioSource>();
    }
    void Start ()
	{
	    v3DebugRay.x = fRayDistance;
	    goBulletStartPosition = transform.GetChild(0).gameObject;
	}
	
	void Update ()
	{
	    RaycastHit2D hit = Physics2D.Raycast(goBulletStartPosition.transform.position, Vector2.right, fRayDistance);
        Debug.DrawRay(goBulletStartPosition.transform.position, v3DebugRay, Color.red);

        if (hit == true
	        && hit.collider.gameObject.CompareTag("Enemy")
	        && !BeeManager.bFormationActive
            && Time.time > fNextShot
            && !sBeeCollision.bIsDead)
	    {
	        Shoot();
	    }
    }

    public void Shoot()
    {
        GameObject newBullet = Instantiate(Resources.Load("BeeStuff/Player/PlayerBullet")) as GameObject;
        newBullet.transform.position = goBulletStartPosition.transform.position;
        fNextShot = Time.time + fFireRate;
        aAnimator.SetTrigger("tShot");
        asSource.PlayOneShot(acShotSound, 1f);
    }
}
