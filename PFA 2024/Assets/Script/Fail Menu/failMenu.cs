using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class failMenu : MonoBehaviour
{
    private string actualLevel;
    [SerializeField] private string mainMenu;
    private void Start()
    {
        actualLevel =SceneManager.GetActiveScene().name;
    }

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
