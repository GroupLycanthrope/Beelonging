using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoneyCounter : MonoBehaviour
{
    private int iHoneyCount;

    private Text tInvincibilityText; 

    void Awake()
    {
        tInvincibilityText = GetComponent<Text>();
        iHoneyCount = 0;
    }

    void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    iHoneyCount = Mathf.RoundToInt(BeeManager.fHoneyCount);
        tInvincibilityText.text = iHoneyCount.ToString();
	}
}
