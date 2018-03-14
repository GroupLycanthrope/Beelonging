using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Playables;
using UnityEngine.UI;

public class EndScoreText : MonoBehaviour
{

    private Text tScoreText;
    // Use this for initialization
    void Start()
    {
        tScoreText = GetComponent<Text>();
        tScoreText.text = "Your Final ScorE: " + ScoreManager.iScore;
    }
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
