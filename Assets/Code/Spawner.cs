using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnData
{
    public float fWaitTime;
    public GameObject xSpawnObject;
}

public class Spawner : MonoBehaviour
{
    //private float fCounter;

    private float fTimer;

    private int i;
    private bool bHasSpawned;


    public List<SpawnData> aSpawnData;

    public GameObject xWinningScreen;
    // Use this for initialization
    void Start()
    {
        i = 0;
        fTimer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(i < aSpawnData.Count) { 
            if (i != 0 && aSpawnData[i-1].xSpawnObject == null) {
                print("test123543");
                fTimer -= Time.deltaTime;
                if(fTimer <= 0) {
                    print("test5");
                    
                    if (aSpawnData[i].xSpawnObject != null) {
                        print("test");
                        GameObject spawn = Instantiate(aSpawnData[i].xSpawnObject, this.transform);
                        spawn.transform.Translate(0, transform.position.y, 0);
                        bHasSpawned = true;
                    }
                    fTimer = aSpawnData[i].fWaitTime;
                    i++;
                }
            }
            else {
                if (fTimer <= 0){
                    print("test5");
                    if (aSpawnData[i].xSpawnObject != null)
                    {
                        print("test");
                        GameObject spawn = Instantiate(aSpawnData[i].xSpawnObject, this.transform);
                        spawn.transform.Translate(0, transform.position.y, 0);
                        bHasSpawned = true;
                    }
                    fTimer = aSpawnData[i].fWaitTime;
                    i++;
                }
            }
            if (!GameObject.FindGameObjectWithTag("Enemy") && bHasSpawned == true)
            {
                print("is this called??");
                aSpawnData[i - 1].xSpawnObject = null;
                bHasSpawned = false;
            }
        }

       
        //if (i < aSpawnData.Count)
        //{
        //    if (fTimer <= 0)
        //    {
        //        if (aSpawnData[i].xSpawnObject != null)
        //        {
        //            GameObject spawn = Instantiate(aSpawnData[i].xSpawnObject, this.transform);
        //            spawn.transform.Translate(0, transform.position.y, 0);
        //        }
        //        i++;
        //        fTimer = aSpawnData[i].fSpawnTime;
        //    }
        //}

        if (!GameObject.FindGameObjectWithTag("Enemy") && i >= aSpawnData.Count && GameObject.FindGameObjectWithTag("Bee"))
        { 
            xWinningScreen.SetActive(true);
        }
    }
}