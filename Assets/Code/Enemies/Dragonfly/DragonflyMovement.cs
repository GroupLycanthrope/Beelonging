using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflyMovement : MonoBehaviour
{
    public float fFlyingSpeed;
    public float fZoomSpeed;

    public Vector2 v2RandomMinInterval;
    public Vector2 v2RandomMaxInterval;

    public float fZoomCooldown;

    private float fNextZoom;

    private float fRandomX;
    private float fRandomY;

    private Vector3 v3ZoomDestination;
    private Vector3 v3ZoomDirection;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Zoom(fZoomCooldown));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
        //transform.Translate(-fFlyingSpeed * Time.deltaTime, 0, 0);

        if (transform.position.x > v3ZoomDestination.x)
        {
            transform.Translate(v3ZoomDirection * (fZoomSpeed * Time.deltaTime));
        }
    }

    IEnumerator Zoom(float p_fZoomCooldown)
    {
        ////RandomizeDestination();
        float fRandomX = Random.Range(v2RandomMinInterval.x, v2RandomMaxInterval.x);
        float fRandomY = Random.Range(v2RandomMinInterval.y, v2RandomMaxInterval.y);
        float fClampedDestinationY = Mathf.Clamp(fRandomY + transform.position.y, -5.5f, 5.5f); //TODO: Make less hard coded
        v3ZoomDestination.x = transform.position.x + fRandomX;
        v3ZoomDestination.y = fClampedDestinationY;
        v3ZoomDirection = v3ZoomDestination.normalized;

        yield return new WaitWhile(() => transform.position.x >= v3ZoomDestination.x);
        //Vector3 v3LastZoomDestination = v3ZoomDestination;
        yield return new WaitForSeconds(p_fZoomCooldown);
        fClampedDestinationY = Mathf.Clamp(transform.position.y + fRandomY * -2, -5.5f, 5.5f); //TODO: Make less hard coded
        v3ZoomDestination.y = fClampedDestinationY; //Reversed and doubled
        v3ZoomDestination.x = transform.position.x + fRandomX;
        v3ZoomDirection = v3ZoomDestination.normalized;
        Debug.Log("New Destination");
        yield return null;

        yield return new WaitWhile(() => transform.position.x >= v3ZoomDestination.x);
        //v3LastZoomDestination = v3ZoomDestination;
        yield return new WaitForSeconds(p_fZoomCooldown);
        fClampedDestinationY = Mathf.Clamp(transform.position.y + fRandomY * -0.5f, -5.5f, 5.5f); //TODO: Make less hard coded
        v3ZoomDestination.y = fClampedDestinationY; //Back to original position
        v3ZoomDestination.x = transform.position.x + fRandomX;
        v3ZoomDirection = v3ZoomDestination.normalized;
        Debug.Log("New Destination");
        yield return null;
    }
}

//void RandomizeDestination()
//    {
//        //fRandomX = Random.Range(v2RandomMinInterval.x, v2RandomMaxInterval.x);
//        //fRandomY = Random.Range(v2RandomMinInterval.y, v2RandomMaxInterval.y);
//        //float fClampedDestinationY = Mathf.Clamp(fRandomY + transform.position.y, -5.5f, 5.5f); //TODO: Make less hard coded
//        //v3ZoomDestination.x = fRandomX;
//        //v3ZoomDestination.y = fClampedDestinationY;
//        //v3ZoomDirection = v3ZoomDestination.normalized;
//        ////Vector3 v3ZoomDestination = new Vector3() { transform.position.x + fRandomX; fClampedDestinationY; 0; }
//        //return v3ZoomDestination;
//    }
//}