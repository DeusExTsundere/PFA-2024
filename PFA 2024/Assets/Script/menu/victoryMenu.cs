using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class victoryMenu : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    [SerializeField] private string mainMenu;
    private string retryLevel;

    private void Start()
    {
        retryLevel = SceneManager.GetActiveScene().name;
    }
    public void nextClick()
    {
        SceneManager.LoadScene(nextLevel);
    }
    public void retryClick()
    {
        SceneManager.LoadScene(retryLevel);
    }
    public void menuClick()
    {
        SceneManager.LoadScene(mainMenu);
    }
}