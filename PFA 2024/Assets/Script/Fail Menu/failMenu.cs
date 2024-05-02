using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class failMenu : MonoBehaviour
{
    [SerializeField] private string actualLevel;
    [SerializeField] private string mainMenu;

    public void retryButton()
    {
        SceneManager.LoadScene(actualLevel);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void quit()
    {
        Application.Quit();
    }
}
