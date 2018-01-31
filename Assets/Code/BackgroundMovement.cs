using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{

    public Material mMaterial;

    public float fScrollingSpeed;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Vector2 v2Result;
	    v2Result = mMaterial.GetTextureOffset("_MainTex");
	    v2Result.x += fScrollingSpeed * Time.deltaTime;
        mMaterial.SetTextureOffset("_MainTex", v2Result);
	}
}
