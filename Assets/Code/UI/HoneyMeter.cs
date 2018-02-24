using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoneyMeter : MonoBehaviour
{
    private BeeManager goBeeManager;

    private Slider sHoneyMeter;
	// Use this for initialization
	void Start ()
	{
	    sHoneyMeter = GetComponent<Slider>();
	    goBeeManager = FindObjectOfType<BeeManager>();
	    sHoneyMeter.maxValue = goBeeManager.fHoneyCountMax;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    sHoneyMeter.value = BeeManager.fHoneyCount;
	}
}
