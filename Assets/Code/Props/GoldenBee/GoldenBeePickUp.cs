using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBeePickUp : MonoBehaviour {

    public float fMoveSpeed;

    float fDeathTimer;
    float fDelay;

    bool bCollidedWithPlayer;
    bool bSpawnBee;

    PolygonCollider2D pcCollider;
    SpriteRenderer spRenderer;

    GameObject AI_Bee;
    GameObject BeeSpawnPos;

    GameObject CreateBee;

    void Start () {
        pcCollider = GetComponent<PolygonCollider2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        fDelay = 10;
        fDeathTimer = fDelay;
        AI_Bee = (GameObject)Resources.Load("BeeStuff/AI/AI_Bee", typeof(GameObject));
        BeeSpawnPos = GameObject.Find("BeeSpawningPos");
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
            CreateBee = Instantiate(AI_Bee);
            CreateBee.transform.position = BeeSpawnPos.transform.position;
            bSpawnBee = true;
            BeeManager.AddBee(CreateBee);
        }

        if (fDeathTimer <= 0){
            Destroy(gameObject);   
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == "Player") {
            bCollidedWithPlayer = true;
        }
    }
}
