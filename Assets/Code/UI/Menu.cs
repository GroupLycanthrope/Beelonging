using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    bool bMenuPressed;
    float fMenuClock;
    float fStartClock;
    GameObject[] storage;
    [HideInInspector]
    public static bool bClawActivation;

    bool bDeactivate;

    public AudioClip button_sound;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start(){
        storage = GameObject.FindGameObjectsWithTag("Storage");
        fMenuClock = 1f;
        fStartClock = 2f;
        bDeactivate = false;

        foreach(GameObject obj in storage) {
            if(obj != null) {
                if (obj.GetComponent<TutorialStorage>().GetTutorialStatus() == true){
                    Destroy(obj);
                }
            }   
        }
    }

    private void Update()
    {
        if (bMenuPressed)
        {
            fMenuClock -= Time.fixedDeltaTime;
        }
        
        if (fMenuClock <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (bDeactivate) {
            fStartClock -= Time.fixedDeltaTime;
            if(fStartClock <= 1) {
                bClawActivation = true;
            }
            if(fStartClock <= 0) {
                
                gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }

    public void SetGameSpeed(float p_fTimeScale)
    {
        Time.timeScale = p_fTimeScale;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        
        bDeactivate = true;
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        bMenuPressed = true;
       
    }

    public void SetVolume(float p_fVolume)
    {
        AudioListener.volume = p_fVolume;
    }

    public void PlaySound()
    {
        source.PlayOneShot(button_sound, 1F);
    }

    IEnumerator FadeIn()
    {
        for (float f = 0f; f < 1; f += 0.1f)
        {
            GameObject fadePanel = GameObject.FindGameObjectWithTag("FadePanel");
            fadePanel.GetComponent<CanvasRenderer>().SetAlpha(f);
            yield return null;
        }

        gameObject.SetActive(true);
    }

    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            GameObject fadePanel = GameObject.FindGameObjectWithTag("FadePanel");
            fadePanel.GetComponent<CanvasRenderer>().SetAlpha(f);
            yield return null;
        }

        gameObject.SetActive(false);
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }
}
