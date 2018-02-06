using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int iScore;

    private Text tScoreText;

    void Awake()
    {
        tScoreText = GetComponent<Text>();
        iScore = 0;
    }

    void Start ()
    {

    }
	
	void Update ()
	{
	    tScoreText.text = "Score: " + iScore;
    }
}
