using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBee : MonoBehaviour {
    public GameObject goPlayerBullet;
    private GameObject goPlayer;

    //Formation variables
    public GameObject goFormationPosition;
    public GameObject[] goListOfFormationPos;
    //Position without formation;
    GameObject[] goListPositions;
    GameObject goPosition;

    PositionInFormation pifFormation;
    RaycastHit2D rhCheckPosition;
    public bool bCallFormation;
    bool bFireshoot;
    public bool bPlayerFireShoot;

    RaycastHit2D rhPlayerSight;

    //used for movement in this obj
    public bool bMoveNow;
    PositionInFormation pifPosition;

    // distance from player to position and player to this object
    float fDistToPos;
    float fDistToMe;
    
    //variables for AI Shooting
    public float fFireRate;
    float fTimer;
    GameObject BulletStartPos;
    //public List<Transform> goEnemies = new List<Transform>();
   // public GameObject[] goEnemies;
    //GameObject goEnemySpawner;
    //public GameObject goTarget;


    private void Awake()
    {
        //GetComponent<Animator>().SetFloat("fAnimationOffset", Random.Range(0, 1));
    }

    // Use this for initialization
    void Start () {
        BulletStartPos = GameObject.Find(this.name+"/BulletPosition");
        fTimer = fFireRate;
	}
	
	// Update is called once per frame
	void Update () {
        if (goPlayer == null) {
            goPlayer = GameObject.Find("Player");
        }
 
        FindPositionInFormation();
        if(bCallFormation == true) {
            MoveToPosition();
        }
        else {
            AvoidPlayer();
        }
    }

    void FindPositionInFormation() {

        if (goFormationPosition == null){
            goListOfFormationPos = GameObject.FindGameObjectsWithTag("Formation");

            goFormationPosition = GameObject.FindGameObjectWithTag("Formation");
        }

        if (goFormationPosition != null){
            rhCheckPosition = Physics2D.Linecast(transform.position, goFormationPosition.transform.position);
            Debug.DrawLine(transform.position, goFormationPosition.transform.position, Color.red);

            if(rhCheckPosition == true) {
            
            if (rhCheckPosition.collider.tag == "Formation"){
                if (pifFormation == null){
                    pifFormation = rhCheckPosition.collider.GetComponent<PositionInFormation>();
                }

                if((pifFormation.sNameOfBee == "" || pifFormation.sNameOfBee == name) && pifFormation.bIsOccupied == false) {
                    pifFormation.sNameOfBee = name;
                    pifFormation.bIsOccupied = true;
                }
                else {
                    for (int i = 0; i < goListOfFormationPos.Length; i++) {
                        if(goListOfFormationPos[i].GetComponent<PositionInFormation>().sNameOfBee == "" && goListOfFormationPos[i].GetComponent<PositionInFormation>().bIsOccupied == false) {
                            goFormationPosition = goListOfFormationPos[i];
                            pifFormation.sNameOfBee = "";
                            pifFormation.bIsOccupied = false;
                            pifFormation = null;
                            break;
                        }
                        if(goListOfFormationPos[i].GetComponent<PositionInFormation>().sNameOfBee == name) {
                            goFormationPosition = goListOfFormationPos[i];
                            pifFormation = null;
                            break;
                        }
                    }

                    
                }

            }
            }
        }
    }

    void MoveToPosition() {
        if(goFormationPosition != null) {
            transform.position = goFormationPosition.transform.position;
        }

        if(bPlayerFireShoot == true) {
            FireShootFormation();
        }

    }

    void AvoidPlayer() {

        if (goPosition == null) {
            goListPositions = GameObject.FindGameObjectsWithTag("Position");
            //Find random Position object 
            goPosition = GameObject.FindGameObjectWithTag("Position");
        }

        if(goPlayer != null) {
            rhPlayerSight = Physics2D.Linecast(transform.position, goPlayer.transform.position);
            Debug.DrawLine(transform.position, goPlayer.transform.position, Color.green);

            if (rhPlayerSight == true && rhPlayerSight.collider.name == "Player"){
                if(rhPlayerSight.distance < 5) {
                    fDistToMe = Vector3.Distance(goPlayer.transform.position, transform.position);
                    for (int i = 0; i < goListPositions.Length; i++){

                        fDistToPos = Vector3.Distance(goPlayer.transform.position, goListPositions[i].transform.position);

                        if ((fDistToPos >= 4 && fDistToMe < 4)){
                            bMoveNow = true;
                        }
                        else{
                            bMoveNow = false;
                        }
                    }
                }
                if(bMoveNow == true) {
                    for(int i = 0; i < goListPositions.Length; i++) {
                        pifPosition = goListPositions[i].GetComponent<PositionInFormation>();
                        if((pifPosition.sNameOfBee == "" || pifPosition.sNameOfBee == name) && pifPosition.bIsOccupied == false) {
                            goPosition = goListPositions[i];
                            transform.position = goPosition.transform.position;
                            pifPosition.sNameOfBee = name;
                            pifPosition.bIsOccupied = true;
                            pifPosition = null;
                            bMoveNow = false;
                            break;
                        }
                    }
                }

            }
        }


        FireShoot();
    }

    void FireShoot() {
        FindTarget();
        if (fTimer < 0) {

        }
    }

    void FindTarget() {
        //goEnemySpawner = GameObject.FindGameObjectWithTag("Spawner");

       // goEnemies = goEnemySpawner.transform.Find("Enemy").gameObjects;

        //if(goEnemies.Length > 0) {
        //    for (int i = 0; i < goEnemies.Length; i++){
        //        if(Mathf.Abs(goEnemies[i].transform.position.y) < Mathf.Abs(transform.position.y + 1)) {
        //            goEnemies[i] = goTarget;
        //        }
        //    }
        //}

    }

    void FireShootFormation() {
        //GetComponent<Animator>().SetTrigger("tShot");
        GameObject bullet = Instantiate(goPlayerBullet);
        bullet.transform.position = BulletStartPos.transform.position;
    }
}
