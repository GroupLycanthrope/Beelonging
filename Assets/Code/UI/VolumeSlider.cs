using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {

    private Slider sVolumeSlider;

    // Use this for initialization
    void Start ()
    {
        sVolumeSlider = GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnValueChanged()
    {
        AudioListener.volume = sVolumeSlider.value;
    }
}
