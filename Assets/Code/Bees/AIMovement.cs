using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public float fSlowSpeed;
    public float fMediumSpeed;
    public float fFastSpeed;

    private float fCurrentSpeed;

    public float fSlowSpeedThreshold;
    public float fMediumSpeedThreshold;

    public Vector2 v2DriftDistanceMin;
    public Vector2 v2DriftDistanceMax;

    public Vector2 v2AIBoundariesMin;
    public Vector2 v2AIBoundariesMax;

    private Vector3 v3SpreadPosition;
    private Vector3 v3Destination;

    [HideInInspector]
    public Vector3 v3Direction;
    
    private GameObject goFormationPosition;

    private Color cLineColor;

    private Animator aAnimator;

    void Start ()
    {
        aAnimator = GetComponent<Animator>();
        aAnimator.SetFloat("fAnimationOffset", Random.Range(0, 1));
        v3Destination = transform.position;
	    v3SpreadPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyUp("x")
	    || BeeManager.fHoneyCount <= 0)
	    {
	        ChangeDestination(v3SpreadPosition);
	    }
        if (BeeManager.bFormationActive)
	    {
	        if ( /*BeeManager.bFormationActive*/
	            Input.GetKeyDown("x")
	            && BeeManager.fHoneyCount > 0)
	        {
	            v3SpreadPosition = v3Destination;
	            goFormationPosition = BeeManager.GetFormationPosition();
	        }
	        

	        if (Input.GetKey("x")
	            && BeeManager.fHoneyCount > 0)
	        {
	            if (goFormationPosition == null)
	            {
	                goFormationPosition = BeeManager.GetFormationPosition();
	            }

	            Vector3 tmp;
	            tmp.x = Mathf.Clamp(goFormationPosition.transform.position.x, v2AIBoundariesMin.x, v2AIBoundariesMax.x);
	            tmp.y = Mathf.Clamp(goFormationPosition.transform.position.y, v2AIBoundariesMin.y, v2AIBoundariesMax.y);
	            tmp.z = 0;
	            ChangeDestination(tmp);
            }

        }
	    else
	    {
	        if (Vector3.Distance(transform.position, v3Destination) <= 0.2)
	        {
	            v3SpreadPosition = FindDriftDestination();
	            ChangeDestination(v3SpreadPosition);
	        }
	    }

	    if (Vector3.Distance(transform.position, v3Destination) >= 0.2)
	    {
	        //if (BeeManager.bFormationActive)
         //   {
         //       fCurrentSpeed = fFastSpeed;
         //       cLineColor = Color.red;
         //   }
            if (Vector3.Distance(transform.position, v3Destination) <= fSlowSpeedThreshold
                     && !BeeManager.bFormationActive)
            {
                fCurrentSpeed = fSlowSpeed;
                cLineColor = Color.green;
            }
            else if (Vector3.Distance(transform.position, v3Destination) <= fMediumSpeedThreshold)
            {
                fCurrentSpeed = fMediumSpeed;
                cLineColor = Color.yellow;
            }
            else if (Vector3.Distance(transform.position, v3Destination) >= fMediumSpeedThreshold)
            {
                fCurrentSpeed = fFastSpeed;
                cLineColor = Color.red;
            }

            //if (Input.GetKey("space"))
            //{
            //    fCurrentSpeed *= 0.3f;
            //}
            //   else if (Input.GetKeyUp("space"))
            //{
            //    fCurrentSpeed /= 0.3f;
	        //}

	        ChangeDirection();
            transform.Translate(v3Direction * fCurrentSpeed * Time.deltaTime);
	    }
	    Debug.DrawLine(transform.position, v3Destination, cLineColor);
    }

    void ChangeDestination(Vector3 p_v3Destination)
    {
        v3Destination = p_v3Destination;
        v3Destination.z = -1;

    }

    void ChangeDirection()
    {
        Mathf.Clamp(v3Destination.x, -7.5f, -2.5f);
        Mathf.Clamp(v3Destination.y, -4.5f, 4.5f);
        v3Direction = (v3Destination - transform.position).normalized;
        v3Direction.z = 0;
    }

    Vector3 FindDriftDestination()
    {
        Vector3 v3DriftDestination;
        float fRandomX = Random.Range(v2DriftDistanceMin.x, v2DriftDistanceMax.x);
        float fRandomY = Random.Range(v2DriftDistanceMin.y, v2DriftDistanceMax.y);
        v3DriftDestination.x = Mathf.Clamp((transform.position.x + fRandomX), -7f, 0f);
        v3DriftDestination.y = Mathf.Clamp((transform.position.y + fRandomY), -4f, 4f);
        v3DriftDestination.z = -1;
        return v3DriftDestination;
    }

    public void ComputeSeparation(Vector3 p_v3AgentDirection)
    {
        v3Destination.x += p_v3AgentDirection.x - v3Direction.x;
        v3Destination.y += p_v3AgentDirection.y - v3Direction.y;

        //v3Destination.x *= -1;
        //v3Destination.y *= -1;
    }
}