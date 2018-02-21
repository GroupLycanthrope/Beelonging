using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAvoidance : MonoBehaviour
{

    private AIMovement sAIMovement;

    void Start ()
    {
        sAIMovement = GetComponentInParent<AIMovement>();
    }
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    void OnTriggerEnter2D(Collider2D p_cOther)
    {
        if (!BeeManager.bFormationActive
        && p_cOther.CompareTag("Bee"))
        {
            if (p_cOther.name == "Player")
            {
                sAIMovement.ComputeSeparation(p_cOther.GetComponent<PlayerController>().v3Direction);
            }
            else
            {
                sAIMovement.ComputeSeparation(p_cOther.GetComponent<AIMovement>().v3Direction);
            }
        }
    }
}
