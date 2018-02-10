using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderProjectileSpriteRotator : MonoBehaviour
{
    public float fRotationSpeed;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.Rotate(0, 0, fRotationSpeed * Time.deltaTime);
    }
}
