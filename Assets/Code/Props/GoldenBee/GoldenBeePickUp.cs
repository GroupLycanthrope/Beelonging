using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBeePickUp : MonoBehaviour {

    public float fMoveSpeed;
    public int iBonusScore;
    public int iScoreValue;
    float fDeathTimer;
    float fDelay;

    public float fSpawnRangeY;

    private GameObject spawn;

    bool bCollidedWithPlayer;
    bool bSpawnBee;

    //PolygonCollider2D pcCollider;
    private CapsuleCollider2D pcCollider;
    SpriteRenderer spRenderer;

    void Start () {
        pcCollider = GetComponent<CapsuleCollider2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        fDelay = 10;
        fDeathTimer = fDelay;
        //BeeSpawnPos = GameObject.Find("BeeSpawningPos");
        bSpawnBee = false;
    }

	void Update () {
        MovePickUp();
        CheckBorder();

        if (bCollidedWithPlayer) {
            CollisionWithPlayer();
        }
	}

    void MovePickUp() {
        transform.Translate(Vector2.left * fMoveSpeed * Time.deltaTime);
    }
    void CheckBorder() {
        if(transform.position.x <= -20) {
            Destroy(gameObject);
        }
    }

    void CollisionWithPlayer() {
        pcCollider.enabled = false;
        spRenderer.enabled = false;
        fDeathTimer -= Time.deltaTime;

        if (!bSpawnBee) {
            //spawn.transform.position = BeeSpawnPos.transform.position;
            if (BeeManager.aSwarm.Count < BeeManager.iSwarmMaxCount)
            {
                spawn = Instantiate(Resources.Load("BeeStuff/AI/AI_Bee") as GameObject);
                this.spawn.transform.position = new Vector3(-9, Random.Range(-fSpawnRangeY, fSpawnRangeY), -1); //-9 becauase it's just off the left border and -1 because all AI bees are on that z value
            }
            else
            {
                ScoreManager.iScore += iBonusScore;
            }

            bSpawnBee = true;
            BeeManager.AddBee(spawn);
        }

        if (fDeathTimer <= 0){
            ScoreManager.iScore += iScoreValue;
            Destroy(gameObject);   
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == "Player") {
            bCollidedWithPlayer = true;
        }
    }
}
