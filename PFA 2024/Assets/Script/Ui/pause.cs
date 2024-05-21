using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    private string currentScene;
    [SerializeField] private string menu;
    [SerializeField] private GameObject[] pauseButton;
    [SerializeField] private GameObject[] settingsButton;
    [SerializeField] private GameObject soundSettings;
    [SerializeField] private GameObject exitSettings;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1f;
    }

    public void Menu()
    {
        SceneManager.LoadScene(menu);
        Time.timeScale = 1f;
    }
    
    public void ExitClick()
    {
        Application.Quit();
    }

    public void SettingsClick()
    {
        for (int i = 0; i < pauseButton.Length; i++)
        {
            pauseButton[i].SetActive(false);
        }
        for (int i = 0;i < settingsButton.Length; i++)
        {
            settingsButton[i].SetActive(true);
        }
        exitSettings.SetActive(true);
    }

    public void soundClick()
    {
        soundSettings.SetActive(true);
        for (int i = 0; i < settingsButton.Length; i++)
        {
            settingsButton[i].SetActive(false);
        }
    }

    public void backSound()
    {
        soundSettings.SetActive(false);
        for (int i = 0; i < settingsButton.Length; i++)
        {
            settingsButton[i].SetActive(true);

        }
    }

    public void ExitSettings()
    {
        for (int i = 0;i < pauseButton.Length;i++)
        {
            pauseButton[i].SetActive(true);
        }
        for (int i = 0; i < settingsButton.Length; i++)
        {
            settingsButton[i].SetActive(false);
        }
        exitSettings.SetActive(false);
    }
}