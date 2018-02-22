using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeeManager : MonoBehaviour
{
    public static int iPowerUpCounter;

    public static bool bIsInvincible;
    public static bool bPlayerDead;

    public static bool bFormationActive;

    public GameObject goGameOverScreen;
    public GameObject goPauseMenu;

    private static List<GameObject> FormationPositions;
    private GameObject goPlayer;

    public static List<GameObject> aSwarm;
    //private GameObject[] aSwarm;

	// Use this for initialization
	void Start ()
	{
	    Time.timeScale = 1;
        iPowerUpCounter = 0;
	    aSwarm = GameObject.FindGameObjectsWithTag("Bee").ToList();
        bIsInvincible = false;
	    bPlayerDead = false;
	    goPlayer = GameObject.Find("Player");
	}

    // Update is called once per frame
    void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
	        if (!goPauseMenu.activeSelf)
	        {
	            goPauseMenu.SetActive(true);
	            Time.timeScale = 0;
	        }
	        else
	        {
	            goPauseMenu.SetActive(false);
	            Time.timeScale = 1;
	        }
	    }
        if (iPowerUpCounter == 3)
	    {
	        bIsInvincible = true;
	        InvincibilityCounter.fInvincibilityTimer -= Time.deltaTime;
	    }
	    if (InvincibilityCounter.fInvincibilityTimer <= 0)
	    {
	        bIsInvincible = false;
	        InvincibilityCounter.fInvincibilityTimer = 5.0f;
	        iPowerUpCounter = 0;
	    }

	    if (aSwarm.Count == 0 
	        && bPlayerDead)
	    {

            goGameOverScreen.SetActive(true);
            //EngineActions.SetGameSpeed(0); 
        }
	    else if (bPlayerDead == true)
	    {
            Respawn();
	    }
    }

    void Respawn()
    {
        //GameObject.Find("Player").GetComponent<BeeCollision>().bIsDead = false;
        int random = Random.Range(1, aSwarm.Count);
        GameObject newPlayer = Instantiate(Resources.Load("BeeStuff/Player/Player")) as GameObject;
        goPlayer = newPlayer;
        goPlayer.transform.position = aSwarm[random - 1].transform.position;
        goPlayer.name = "Player";
        KillBeell(aSwarm[random - 1]);
        bPlayerDead = false;
    }

    public static void KillBeell(GameObject p_goDeadBee)
    {
        
        p_goDeadBee.GetComponent<SpriteRenderer>().enabled = false;
        p_goDeadBee.GetComponent<CapsuleCollider2D>().enabled = false;
        p_goDeadBee.GetComponent<BeeCollision>().bIsDead = true;
        aSwarm.Remove(p_goDeadBee);
        if (p_goDeadBee.name == "Player")
        {
            bPlayerDead = true;
        }
        Destroy(p_goDeadBee, 1);
    }

    public static void AddBee(GameObject p_goNewBee)
    {
        aSwarm.Add(p_goNewBee);
    }

    public static GameObject GetFormationPosition()
    {
        FormationPositions = GameObject.FindGameObjectsWithTag("Formation").ToList();
        
        for (int i = 0; i < FormationPositions.Count; i++)
        {
            if (FormationPositions[i].GetComponent<PositionInFormation>().bIsOccupied == false)
            {
                FormationPositions[i].GetComponent<PositionInFormation>().bIsOccupied = true;
                return FormationPositions[i];
            }
        }
        return FormationPositions[0]; //Fallback if things go wrong
    }

    public static void UnOccupyPositions()
    {
        if (aSwarm.Count > 1)
        {
            for (int i = 0; i < FormationPositions.Count; i++)
            {
                FormationPositions[i].GetComponent<PositionInFormation>().bIsOccupied = false;
            }
        }
    }
}