using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoneyMeter : MonoBehaviour
{
    private BeeManager goBeeManager;

    private float fGoalCount;
    public float fTickUpRate;

    private Slider sHoneyMeter;
	// Use this for initialization
	void Start ()
	{
	    sHoneyMeter = GetComponent<Slider>();
	    goBeeManager = FindObjectOfType<BeeManager>();
	    sHoneyMeter.maxValue = goBeeManager.fHoneyCountMax;
	}
	
	// Update is called once per frame
	void Update (){
        fGoalCount = BeeManager.fHoneyCount;
        if (sHoneyMeter.value < fGoalCount) {
            sHoneyMeter.value += fTickUpRate;
        }
        if(sHoneyMeter.value > fGoalCount) {
            sHoneyMeter.value -= fTickUpRate;
        }
            
            
	}
}
