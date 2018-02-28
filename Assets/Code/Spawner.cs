using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnData
{
    public float fSortID;
    public float fWaitTime;
    public GameObject xSpawnObject;
}

[ExecuteInEditMode]
public class Spawner : MonoBehaviour{
    private float fTimer;

    static public int iSpawnerAt;

    private bool bHasSpawned;
    public bool bHasSortList;

    public List<SpawnData> aSpawnData;
    static private List<SpawnData> aCopyofSpawnData;

    public GameObject xWinningScreen;

    void Start(){
        iSpawnerAt = 0;
        fTimer = aSpawnData[iSpawnerAt].fWaitTime;
        bHasSortList = false;
        aCopyofSpawnData = aSpawnData;
    }

    void Update(){
        if (Application.isPlaying){
            if (iSpawnerAt < aSpawnData.Count){
                // check if the previous wave is null before creating the next one
                if (iSpawnerAt != 0 && aSpawnData[iSpawnerAt - 1].xSpawnObject == null){
                    // start to count down to zero
                    fTimer -= Time.deltaTime;
                    if (fTimer <= 0){
                        // Spawn the enemy wave
                        if (aSpawnData[iSpawnerAt].xSpawnObject != null){
                            GameObject spawn = Instantiate(aSpawnData[iSpawnerAt].xSpawnObject, this.transform);
                            spawn.transform.Translate(0, transform.position.y, 0);
                            bHasSpawned = true;
                        }
                        
                        iSpawnerAt++;
                        // had to add this in because random error dont know why but this fixes the error
                        if(iSpawnerAt != aSpawnData.Count) { 
                        fTimer = aSpawnData[iSpawnerAt].fWaitTime;
                        }

                    }
                }
                else{
                    // this only happens on the first wave because aSpawnData[0-1] is not valid and the rest is just a copied from before
                    if (iSpawnerAt == 0){
                        fTimer -= Time.deltaTime;
                        if (fTimer <= 0){
                            if (aSpawnData[iSpawnerAt].xSpawnObject != null){
                                GameObject spawn = Instantiate(aSpawnData[iSpawnerAt].xSpawnObject, this.transform);
                                spawn.transform.Translate(0, transform.position.y, 0);
                                bHasSpawned = true;
                            }
                            iSpawnerAt++;
                            fTimer = aSpawnData[iSpawnerAt].fWaitTime;
                        }
                    }
                }
                // checks after enemies and if a wave have been spawned it sets the previous wave to null so the next wave can spawn
                if (!GameObject.FindGameObjectWithTag("Enemy") && bHasSpawned == true && !GameObject.FindGameObjectWithTag("HoneycombPickUp")){
                    aSpawnData[iSpawnerAt - 1].xSpawnObject = null;
                    bHasSpawned = false;
                }
            }

            if (!GameObject.FindGameObjectWithTag("Enemy") && !GameObject.FindGameObjectWithTag("HoneycombPickUp") && iSpawnerAt >= aSpawnData.Count && GameObject.FindGameObjectWithTag("Bee"))
            {
                xWinningScreen.SetActive(true);
            }
        }
        // this only happens in the Editor not when the game is played minimizes lag
#if UNITY_EDITOR
        // extra safe guard because unity is a cunt sometimes
        if (!Application.isPlaying) {
            SortList();
        }
#endif
    }
    // this has to be an int otherwise noting work IDK why and it sort properly as it would be a float, again programming is magic
    static int SortBySortID(SpawnData p1, SpawnData p2){
        return p1.fSortID.CompareTo(p2.fSortID);
       
    }

    void SortList() {
        // sort the array (list) with the function SortBySortID by comparing two floats (which is an int but work like an int don't know why) Oh btw I have litterly no clue how this sort and how it even work... but it does ¯\_(ツ)_/¯. It was a wild guess from my side by implementing two different tutorials 
        aSpawnData.Sort(SortBySortID);
    }

    static public int GetTotalWaves() {
        
        return aCopyofSpawnData.Count;
    }

    static public int GetWaveAt(){
        return iSpawnerAt;
    }
    
    static public float GetProcentOfWave(int p_iWave) {
        return (p_iWave / aCopyofSpawnData.Count);
    }
}