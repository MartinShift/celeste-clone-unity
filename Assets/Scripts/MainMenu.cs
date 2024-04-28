using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int PreviousScene = 1;

    public void StartGame()
    {
        SceneManager.LoadScene(PreviousScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
