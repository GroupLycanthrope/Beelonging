using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveSpawnData
{
    public Vector2 v2DeltaPosition;
    public GameObject xSpawnObject;
}
public class WaveSpawner : MonoBehaviour
{
    public List<WaveSpawnData> aWaveSpawnData;

	// Use this for initialization
	void Start ()
	{
	    for (int i = 0 ; i < aWaveSpawnData.Capacity ; i++)
	    {
	        GameObject spawn = Instantiate(aWaveSpawnData[i].xSpawnObject, this.transform);
            spawn.transform.Translate(aWaveSpawnData[i].v2DeltaPosition);
        }
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
