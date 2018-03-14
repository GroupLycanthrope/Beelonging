using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerSound : MonoBehaviour {

    private AudioSource source;
    public float fLowerSoundRate;
    bool bLowerVolume;

	// Use this for initialization
	void Start () {
        source = gameObject.GetComponent<AudioSource>();
        bLowerVolume = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (bLowerVolume) {
            source.volume -= fLowerSoundRate;
        }
	}

    public void LowerVolume(bool p_YesOrNo) {
        bLowerVolume = p_YesOrNo;
    }
}
