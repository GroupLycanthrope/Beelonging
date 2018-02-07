using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlySpawnCoordinates
{
    public Vector2 v2DeltaPosition;
}
public class FlyFormationSpawner : MonoBehaviour
{
    public GameObject xFly;
    public List<FlySpawnCoordinates> aFlySpawnCoordinates;

	// Use this for initialization
	void Start ()
	{
	    for (int i = 0 ; i < aFlySpawnCoordinates.Capacity ; i++)
	    {
	        var fly = Instantiate(xFly, this.transform);
            fly.transform.Translate(aFlySpawnCoordinates[i].v2DeltaPosition);
        }
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
