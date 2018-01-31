using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EngineActions : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}