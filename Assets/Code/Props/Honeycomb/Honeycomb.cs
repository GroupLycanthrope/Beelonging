using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Honeycomb : MonoBehaviour
{

    public float fFloatingSpeed;
    float fShrinkRate;

    public int iHoneyValue;

    GameObject gTargetPosWhenCollidedWith;

    bool bHasCollided;
    bool bCalculatedShrinkRate;

    Vector3 v3ChangeScale;
	// Use this for initialization
	void Start (){
        gTargetPosWhenCollidedWith = GameObject.Find("HoneycombGoTo");
        bHasCollided = false;
        bCalculatedShrinkRate = false;
    }
	
	// Update is called once per frame
	void Update (){
        if (!bHasCollided) {
            Move();
        }
	    
        if (bHasCollided) {
            GoToBar();
        }
	}

    void Move(){
        transform.Translate(-fFloatingSpeed * Time.deltaTime, 0, 0);
    }

    void GoToBar() {
        float fStep = (fFloatingSpeed * 3) * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, gTargetPosWhenCollidedWith.transform.position, fStep);

        if (!bCalculatedShrinkRate) {
            fShrinkRate = fStep/ (Vector3.Distance(transform.position, gTargetPosWhenCollidedWith.transform.position));
            fShrinkRate = fShrinkRate / 8;
            v3ChangeScale.x = fShrinkRate;
            v3ChangeScale.y = fShrinkRate;
            bCalculatedShrinkRate = true;
        }
        if(transform.localScale.x > 0.01f) {
            transform.localScale -= v3ChangeScale;
        }

        if(transform.position.x == gTargetPosWhenCollidedWith.transform.position.x) {
            if (BeeManager.fHoneyCount < FindObjectOfType<BeeManager>().fHoneyCountMax){
                BeeManager.fHoneyCount += iHoneyValue;
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D p_xOtherCollider)
    {
        if (p_xOtherCollider.gameObject.CompareTag("Bee"))
        {
            
            bHasCollided = true;
            
            Debug.Log(BeeManager.fHoneyCount.ToString());
            
        }
    }
}
