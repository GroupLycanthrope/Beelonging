using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EngineActions : MonoBehaviour
{
    public static void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }

    public static void SetGameSpeed(float p_fTimeScale)
    {
        Time.timeScale = p_fTimeScale;
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}