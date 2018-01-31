using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeCollision : MonoBehaviour {

    public AudioClip deathsound;
    private AudioSource source;
    public GameObject This;

    private void Awake(){
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        if (p_xOtherCollider.gameObject.CompareTag("Enemy"))
        {
            source.PlayOneShot(deathsound, 1F);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            //TODO: Death Animation plus promoting new bee
            This.GetComponent<ControlHealth>().setHealth(-1);
            //Destroy(gameObject, 1);
        }
    }
}
