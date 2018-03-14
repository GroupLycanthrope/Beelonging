using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroller : MonoBehaviour
{
    public GameObject goCreditsPanel;

    public GameObject goMainMenu;

    public float fScrollingSpeed;

    public float fExitTime;
    public float fStartTime;

    private float fTimer;

    private float fExitTimer;

    private bool bHasReachedEnd = false;

    // Use this for initialization
    void Start()
    {
        bHasReachedEnd = false;
        Time.timeScale = 1;
        Vector3 tmp;
        tmp.x = 960;
        tmp.y = -1920;
        tmp.z = 0;
        goCreditsPanel.transform.position = tmp;
        fTimer = fStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (fTimer >= 0)
        {
            fTimer -= Time.deltaTime;
        }
        else
        {
            if (goCreditsPanel.transform.position.y < 2990)
            {
                goCreditsPanel.transform.Translate(0, fScrollingSpeed, 0);
            }
            else
            {
                fExitTimer = fExitTime;
                bHasReachedEnd = true;
                //Invoke("GoToMainMenu", fExitTime);
            }
        }

        if(fExitTimer > 0)
        {
            fExitTimer -= Time.deltaTime;
        }
        else if (bHasReachedEnd)
        {
            GoToMainMenu();
        }

        if (Input.anyKeyDown)
        {
            GoToMainMenu();
        }
    }

    void GoToMainMenu()
    {
        if (GameObject.Find("CreditsPanel") != null)
        {
            GameObject.Find("CreditsPanel").SetActive(false);
            Time.timeScale = 0;
            goMainMenu.SetActive(true);
        }
    }

    void OnDestroy()
    {
        bHasReachedEnd = false;
        Vector3 tmp;
        tmp.x = 960;
        tmp.y = -1920;
        tmp.z = 0;
        goCreditsPanel.transform.position = tmp;
    }

    private void OnDisable()
    {
        fTimer = fStartTime;
        Time.timeScale = 0;
        Vector3 tmp;
        tmp.x = 960;
        tmp.y = -1920;
        tmp.z = 0;
        goCreditsPanel.transform.position = tmp;
    }

    private void OnEnable()
    {
        bHasReachedEnd = false;
        fTimer = fStartTime;
        Time.timeScale = 1;
        Vector3 tmp;
        tmp.x = 960;
        tmp.y = -1920;
        tmp.z = 0;
        goCreditsPanel.transform.position = tmp;

    }
}
