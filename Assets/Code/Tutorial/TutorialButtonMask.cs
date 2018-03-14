using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtonMask : MonoBehaviour
{
    public GameObject goTutorialButton;

    private TutorialButton TutorialButton;

    // Use this for initialization
	void Start ()
	{
        TutorialButton = goTutorialButton.GetComponent<TutorialButton>();
	    //TutorialButton = GameObject.FindGameObjectWithTag("Tutorial").GetComponent<TutorialButton>();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (TutorialButton.GetTimer() > 0)
        {
            Vector3 tmp = transform.localScale;
            tmp.y = (TutorialButton.fKeyDownTimer - TutorialButton.GetTimer()) / TutorialButton.fKeyDownTimer;
            transform.localScale = tmp;
        }
    }
}
