using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EngineActions : MonoBehaviour
{
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
}