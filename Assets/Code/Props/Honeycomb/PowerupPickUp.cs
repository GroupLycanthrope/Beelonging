using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupPickUp : MonoBehaviour
{


    public GameObject xLeftHoneyComb;
    public GameObject xMiddleHoneyComb;
    public GameObject xRightHoneyComb;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (BeeManager.fHoneyCount == 1)
	    {
	        Image tempImage = xLeftHoneyComb.GetComponent<Image>();
	        var tempColor = tempImage.color;
	        tempColor.a = 1f;
	        tempImage.color = tempColor;
	    }
	    else if (BeeManager.fHoneyCount == 2)
	    {
	        Image tempImage = xMiddleHoneyComb.GetComponent<Image>();
	        var tempColor = tempImage.color;
	        tempColor.a = 1f;
	        tempImage.color = tempColor;
	    }
	    else if (BeeManager.fHoneyCount == 3)
	    {
	        Image tempImage = xRightHoneyComb.GetComponent<Image>();
	        var tempColor = tempImage.color;
	        tempColor.a = 1f;
	        tempImage.color = tempColor;
	    }
	    else if (BeeManager.fHoneyCount == 0)
	    {
	        Image tempLeftImage = xLeftHoneyComb.GetComponent<Image>();
	        var tempLeftColor = tempLeftImage.color;
	        tempLeftColor.a = 0.3f;
	        tempLeftImage.color = tempLeftColor;

	        Image tempMiddleImage = xMiddleHoneyComb.GetComponent<Image>();
	        var tempMiddleColor = tempMiddleImage.color;
            tempMiddleColor.a = 0.3f;
            tempMiddleImage.color = tempMiddleColor;

	        Image tempRightImage = xRightHoneyComb.GetComponent<Image>();
	        var tempRightColor = tempRightImage.color;
            tempRightColor.a = 0.3f;
            tempRightImage.color = tempRightColor;
        }
    }
}
