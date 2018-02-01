using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvincibilityCounter : MonoBehaviour
{

    public static float fInvincibilityTimer;

    private Text tInvincibilityText; 

    void Awake()
    {
        tInvincibilityText = GetComponent<Text>();
        fInvincibilityTimer = 5.0f;
    }

    void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    fInvincibilityTimer = Mathf.Round(fInvincibilityTimer * 100f) / 100f;
        tInvincibilityText.text = fInvincibilityTimer.ToString();
	}
}
