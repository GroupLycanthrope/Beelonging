using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroller : MonoBehaviour
{
    public GameObject goCreditsPanel;

    public float fScrollingSpeed;

    public float fExitTime;
    public float fStartTime;

    private float fTimer;

    // Use this for initialization
    void Start()
    {
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
                Invoke("GoToMainMenu", fExitTime);
            }
        }

        if (Input.anyKey)
        {
            GoToMainMenu();
        }
    }

    void GoToMainMenu()
    {
        GameObject.Find("CreditsPanel").SetActive(false);
        Time.timeScale = 0;
    }

    void OnDestroy()
    {
        Vector3 tmp;
        tmp.x = 960;
        tmp.y = -1920;
        tmp.z = 0;
        goCreditsPanel.transform.position = tmp;
    }

    private void OnDisable()
    {
        Time.timeScale = 0;
        Vector3 tmp;
        tmp.x = 960;
        tmp.y = -1920;
        tmp.z = 0;
        goCreditsPanel.transform.position = tmp;
    }

    private void OnEnable()
    {
        Time.timeScale = 1;
        Vector3 tmp;
        tmp.x = 960;
        tmp.y = -1920;
        tmp.z = 0;
        goCreditsPanel.transform.position = tmp;
        fTimer = fStartTime;
    }
}
