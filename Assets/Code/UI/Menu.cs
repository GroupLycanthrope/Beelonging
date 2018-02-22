using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
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
        Time.timeScale = 1;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("LövliesSandbox");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetVolume(float p_fVolume)
    {
        AudioListener.volume = p_fVolume;
    }
}
