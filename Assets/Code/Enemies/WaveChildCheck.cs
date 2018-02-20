using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveChildCheck : MonoBehaviour {

    public GameObject[] goEnemiesInFormation;

    float ftimer;
    float ftimeLeft = 1;

	// Use this for initialization
	void Start () {
        goEnemiesInFormation = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		if(goEnemiesInFormation.Length == 0) {
            Destroy(gameObject);
        }
        if(ftimer <= 0) {
            goEnemiesInFormation = GameObject.FindGameObjectsWithTag("Enemy");
            ftimer = ftimeLeft;
        }
        ftimer -= Time.deltaTime;
	}
}
