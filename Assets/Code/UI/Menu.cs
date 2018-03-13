using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public bool bMenuPressed;
    public float fClock;

    GameObject[] storage;
    [HideInInspector]
    public static bool bClawActivation;

    public AudioClip button_sound;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start(){
        storage = GameObject.FindGameObjectsWithTag("Storage");
        fClock = 1f;
        


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
            fClock -= Time.fixedDeltaTime;
        }
        if (fClock <= 0)
        {
            SceneManager.LoadScene("MainMenu");
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
        bClawActivation = true;
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
}
