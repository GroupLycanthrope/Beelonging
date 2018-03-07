using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTutorial : MonoBehaviour {

    TutorialButton tutorial;

	// Use this for initialization
	void Start () {
        tutorial = GetComponent<TutorialButton>();
	}
	
	// Update is called once per frame
	void Update () {
        if (tutorial.GetIsPressed()) {
            Spawner.SetTutorial(true);
        }
	}
}
