using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnData
{
    public float fSpawnY;
    public float fSpawnTime;
    public GameObject xSpawnObject;
}

public class EnemySpawner : MonoBehaviour
{
    //private float fCounter;

    private float fTimer;

    private int i;
    
    public List<SpawnData> aSpawnData;

    // Use this for initialization
    void Start()
    {
        i = 0;
        fTimer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        //fCounter += Time.deltaTime;
        //if (fCounter > fTimer)
        //{
        //       SpawnFlies();
        //       SpawnPickUp();
        //    fCounter = 0;
        //}
        fTimer += Time.deltaTime;
        if (i < aSpawnData.Count)
        {
            if (aSpawnData[i].fSpawnTime <= fTimer)
            {
                if (aSpawnData[i].xSpawnObject != null)
                {
                    GameObject Spawn = Instantiate(aSpawnData[i].xSpawnObject, this.transform);
                    Spawn.transform.Translate(0, aSpawnData[i].fSpawnY, 0);
                }
                i++;
            }
        }
    }

    //void SpawnFlies()
    //{
    //    Vector2 v2SwarmCenter = this.transform.position;
    //    v2SwarmCenter.y = Random.Range(-4, 4);
    //    GameObject fly1 = Instantiate(xFly);
    //    fly1.transform.position = v2SwarmCenter;
    //    GameObject fly2 = Instantiate(xFly);
    //    fly2.transform.position = v2SwarmCenter;
    //    fly2.transform.Translate(1, 0, 0);
    //    GameObject fly3 = Instantiate(xFly);
    //    fly3.transform.position = v2SwarmCenter;
    //    fly3.transform.Translate(0, 1, 0);
    //    GameObject fly4 = Instantiate(xFly);
    //    fly4.transform.position = v2SwarmCenter;
    //    fly4.transform.Translate(0, -1, 0);
    //    GameObject fly5 = Instantiate(xFly);
    //    fly5.transform.position = v2SwarmCenter;
    //    fly5.transform.Translate(-1, 0, 0);
    //}

    //void SpawnPickUp()
    //{
    //    GameObject honeyCombPickUp = Instantiate(xHoneyCombPickUp);
    //    Vector2 v2SpawnLocation = this.transform.position;
    //    v2SpawnLocation.y = Random.Range(-4, 4);
    //    honeyCombPickUp.transform.position = v2SpawnLocation;
    //}
}