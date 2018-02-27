using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeeManager : MonoBehaviour
{
    public float fHoneyCountMax;
    public float fHoneyStartCount;
    public float fHoneyCountDrain;
    public static float fHoneyCount;

    public int iSwarmMax;
    public static int iSwarmMaxCount;

    //public static bool bIsInvincible;
    public static bool bPlayerDead;

    public static bool bFormationActive;

    public GameObject goGameOverScreen;
    public GameObject goPauseMenu;

    private static List<GameObject> aFormationPositions;
    private GameObject goPlayer;

    public static List<GameObject> aSwarm;

	// Use this for initialization
	void Start ()
	{
	    iSwarmMaxCount = iSwarmMax;
	    Time.timeScale = 1;
        fHoneyCount = fHoneyStartCount;
        aSwarm = GameObject.FindGameObjectsWithTag("Bee").ToList();
        aFormationPositions = GameObject.FindGameObjectsWithTag("Formation").ToList();

        //bIsInvincible = false;
        bPlayerDead = false;
	    goPlayer = GameObject.Find("Player");
	}

    // Update is called once per frame
    void Update ()
	{
	    if (fHoneyCount < 0)
	    {
	        bFormationActive = false;
	    }
        if (bFormationActive)
	    {
	        fHoneyCount -= fHoneyCountDrain * Time.deltaTime;
	    }
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
    
        //   if (fHoneyCount == 3)
	    //{
	    //    bIsInvincible = true;
	    //    HoneyCounter.fInvincibilityTimer -= Time.deltaTime;
	    //}
	    //if (HoneyCounter.fInvincibilityTimer <= 0)
	    //{
	    //    bIsInvincible = false;
	    //    HoneyCounter.fInvincibilityTimer = 5.0f;
	    //    fHoneyCount = 0;
	    //}

	    if (aSwarm.Count == 0 
	        && bPlayerDead)
	    {
            goGameOverScreen.SetActive(true);
        }
	    else if (bPlayerDead == true)
	    {
            Respawn();
	    }

	    
    }

    void Respawn()
    {
        aFormationPositions = GameObject.FindGameObjectsWithTag("Formation").ToList();
        int random = Random.Range(1, aSwarm.Count);
        GameObject newPlayer = Instantiate(Resources.Load("BeeStuff/Player/Player")) as GameObject;
        goPlayer = newPlayer;
        goPlayer.gameObject.SendMessage("StartSpriteFlasher");
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
        for (int i = 0; i < aSwarm.Count; i++)
        {
            aSwarm[i].SendMessage("StartSpriteFlasher");
        }
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
        aFormationPositions = GameObject.FindGameObjectsWithTag("Formation").ToList();
        
        for (int i = 0; i < aFormationPositions.Count; i++)
        {
            if (aFormationPositions[i].GetComponent<PositionInFormation>().bIsOccupied == false)
            {
                aFormationPositions[i].GetComponent<PositionInFormation>().bIsOccupied = true;
                return aFormationPositions[i];
            }
        }
        return aFormationPositions[0]; //Fallback if things go wrong
    }

    public static void UnOccupyPositions()
    {
        if (aSwarm.Count > 1
            && !bPlayerDead)
        {
            if (aFormationPositions != null)
            {
                for (int i = 0; i < aFormationPositions.Count; i++)
                {
                    if (aFormationPositions.Count > 0)
                    { 
                        if (aFormationPositions[i] != null)
                        {
                            aFormationPositions[i].GetComponent<PositionInFormation>().bIsOccupied = false;
                        }
                    }
                }
            }
        }
    }
}