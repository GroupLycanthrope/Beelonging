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

    private GameObject goPlayer;
    private GameObject goParticles;

    private static List<GameObject> aFormationPositions;
    public static List<GameObject> aSwarm;

    static private Vector3 v3MaxCameraBorders;
    static private Vector3 v3MinCameraBorder;

	// Use this for initialization
	void Start ()
	{
        v3MaxCameraBorders = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        v3MinCameraBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        iSwarmMaxCount = iSwarmMax;
	    Time.timeScale = 1;
        fHoneyCount = fHoneyStartCount;
        aSwarm = GameObject.FindGameObjectsWithTag("Bee").ToList();
        aFormationPositions = GameObject.FindGameObjectsWithTag("Formation").ToList();
        goParticles = (GameObject)Resources.Load("Props/ParticleSystem", typeof(GameObject));
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

	    if (fHoneyCount > fHoneyCountMax)
	    {
	        fHoneyCount = fHoneyCountMax;
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
        Destroy(GameObject.Find("FormationParent"));
        int randomIndex = Random.Range(0, aSwarm.Count - 1);
        GameObject newPlayer = Instantiate(Resources.Load("BeeStuff/Player/Player")) as GameObject;
        AddBee(newPlayer);
        goPlayer = newPlayer;
        aFormationPositions = GameObject.FindGameObjectsWithTag("Formation").ToList();
        goPlayer.gameObject.SendMessage("StartSpriteFlasher");
        goPlayer.transform.position = aSwarm[randomIndex].transform.position;
        goPlayer.name = "Player";

        GameObject Particles = Instantiate(goParticles);
        Particles.transform.position = goPlayer.transform.position;

        Destroy(aSwarm[randomIndex]);
        aSwarm.Remove(aSwarm[randomIndex]);
        //if(aSwarm[randomIndex] != null)
        bPlayerDead = false;
    }

    public static void KillBeell(GameObject p_goDeadBee)
    {
        for (int i = 0; i < aSwarm.Count; i++)
        {
            if (aSwarm[i] != null)
            {
                aSwarm[i].SendMessage("StartSpriteFlasher");
            }
        }

        if (p_goDeadBee.name == "Player")
        {
            bPlayerDead = true;
        }
        aSwarm.Remove(p_goDeadBee);
        //Destroy(p_goDeadBee, 1);
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
    // getting the borders for the camera
    public static Vector3 GetMaxCameraBorder(){
        return v3MaxCameraBorders;
    }
    public static Vector3 GetMinCameraBorder(){
        return v3MinCameraBorder;
    }
}