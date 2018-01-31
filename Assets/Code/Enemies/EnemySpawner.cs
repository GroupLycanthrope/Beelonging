using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float fCounter;

    public float fTimer;

    public GameObject xFly;
    public GameObject xHoneyCombPickUp;


	// Use this for initialization
	void Start ()
	{
	    SpawnFlies();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    fCounter += Time.deltaTime;
	    if (fCounter > fTimer)
	    {
            SpawnFlies();
            SpawnPickUp();
	        fCounter = 0;
	    }
	}

    void SpawnFlies()
    {
        Vector2 v2SwarmCenter = this.transform.position;
        v2SwarmCenter.y = Random.Range(-4, 4);
        GameObject fly1 = Instantiate(xFly);
        fly1.transform.position = v2SwarmCenter;
        GameObject fly2 = Instantiate(xFly);
        fly2.transform.position = v2SwarmCenter;
        fly2.transform.Translate(1, 0, 0);
        GameObject fly3 = Instantiate(xFly);
        fly3.transform.position = v2SwarmCenter;
        fly3.transform.Translate(0, 1, 0);
        GameObject fly4 = Instantiate(xFly);
        fly4.transform.position = v2SwarmCenter;
        fly4.transform.Translate(0, -1, 0);
        GameObject fly5 = Instantiate(xFly);
        fly5.transform.position = v2SwarmCenter;
        fly5.transform.Translate(-1, 0, 0);
    }

    void SpawnPickUp()
    {
        GameObject honeyCombPickUp = Instantiate(xHoneyCombPickUp);
        Vector2 v2SpawnLocation = this.transform.position;
        v2SpawnLocation.y = Random.Range(-4, 4);
        honeyCombPickUp.transform.position = v2SpawnLocation;
    }
}
