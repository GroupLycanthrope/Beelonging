using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnData{
    public bool bAllDespawned;
    public bool bAllKilled;
    public int iSkipEntries;
    public float fSortID;
    public float fWaitTime;
    public GameObject xSpawnObject;
}

[ExecuteInEditMode]
public class Spawner : MonoBehaviour{

    float fTimer;

    static public int iSpawnerAt;

    private bool bControllBool;
    public bool bHasList;

    private bool bNewWave;
    private bool bIsScreenEmpty;
    private bool bHasSortList;
    
    bool bAllEnemiesDied;
    bool bAllEnemiesDespawned;

    bool[] abAllEnemiesDespawned;
    GameObject[] aAllEnemies;
    GameObject[] aAllPowerUps;

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
                if (bNewWave && bIsScreenEmpty) {
                    fTimer -= Time.deltaTime;
                    if (fTimer <= 0) {

                        if(iSpawnerAt != 0 && aSpawnData[iSpawnerAt].xSpawnObject != null) {
                            if (!bAllEnemiesDespawned && aSpawnData[iSpawnerAt-1].bAllDespawned || !bAllEnemiesDied && aSpawnData[iSpawnerAt-1].bAllKilled){
                                iSpawnerAt += aSpawnData[iSpawnerAt-1].iSkipEntries;
                                abAllEnemiesDespawned = new bool[0];
                                aAllEnemies = new GameObject[0];
                            }
                        }
                        

                        if (aSpawnData[iSpawnerAt].xSpawnObject != null){
                            GameObject spawn = Instantiate(aSpawnData[iSpawnerAt].xSpawnObject, this.transform);
                            spawn.transform.Translate(0, transform.position.y, 0);
                            bNewWave = false;
                            bHasList = false;
                        }
                         iSpawnerAt++;
                        // had to add this in because random error dont know why but this fixes the error
                        if (iSpawnerAt != aSpawnData.Count){
                            fTimer = aSpawnData[iSpawnerAt].fWaitTime;
                        }
                    }
                }

                if (!bNewWave && bIsScreenEmpty){
                    bNewWave = true;
                }


                if (iSpawnerAt != 0){

                    if (!bHasList && (GameObject.FindGameObjectWithTag("Enemy") || GameObject.FindGameObjectWithTag("PickUp"))) {
                        aAllEnemies = GameObject.FindGameObjectsWithTag("Enemy");

                        aAllPowerUps = GameObject.FindGameObjectsWithTag("PickUp");

                        abAllEnemiesDespawned = new bool[aAllEnemies.Length + aAllPowerUps.Length];
                        bHasList = true;
                    }

                    if (aSpawnData[iSpawnerAt - 1].bAllKilled == true || aSpawnData[iSpawnerAt - 1].bAllDespawned == true){
                        if(aAllEnemies != null) {
                            for (int i = 0; i < aAllEnemies.Length; i++){
                                if (aAllEnemies[i] != null && aAllEnemies[i].transform.position.x <= BeeManager.GetMinCameraBorder().x){
                                    abAllEnemiesDespawned[i] = true;
                                }
                            }
                        }
                        
                        if (bNewWave) {
                            if(abAllEnemiesDespawned != null) {
                                foreach (bool despawned in abAllEnemiesDespawned){
                                    if (despawned == true){
                                        bAllEnemiesDespawned = true;
                                    }
                                    else{
                                        bAllEnemiesDespawned = false;
                                        break;
                                    }
                                }
                                foreach (bool killed in abAllEnemiesDespawned){
                                    if (killed == false){
                                        bAllEnemiesDied = true;
                                    }
                                    else{
                                        bAllEnemiesDied = false;
                                        break;
                                    }
                                }
                            }

                        }
                    }
                }

            }

            if (!GameObject.FindGameObjectWithTag("Enemy") && !GameObject.FindGameObjectWithTag("PickUp")){
                bIsScreenEmpty = true;
            }
            else {
                bIsScreenEmpty = false;
            }

            




            if (!GameObject.FindGameObjectWithTag("Enemy") && !GameObject.FindGameObjectWithTag("PickUp") && iSpawnerAt >= aSpawnData.Count && GameObject.FindGameObjectWithTag("Bee") && !GameObject.FindGameObjectWithTag("Wasp"))
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
        return ((float)p_iWave / (float)aCopyofSpawnData.Count) * 100;
    }
}