using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public float fScrollingSpeed;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate(-fScrollingSpeed * Time.deltaTime, 0, 0);
	}
}