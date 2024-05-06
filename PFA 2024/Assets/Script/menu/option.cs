using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class option : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private CanvasGroup mainMenu;
    [SerializeField] private GameObject menu;
    [SerializeField] private CanvasGroup settingsMenu;
    [SerializeField] private GameObject setting;
    [SerializeField] private CanvasGroup creditsMenu;
    [Header("Settings Menu")]
    [SerializeField] private CanvasGroup soundMenu;
    [SerializeField] private CanvasGroup controleMenu;
    [Header("Difficulty Menu")]
    [SerializeField] private CanvasGroup difficultyMenu;
    [SerializeField] private GameObject difficulty;

    private float tempo = 0.9f;
    private float tempoDesactivation = 1f;
    private bool mainMenuEnabled = true;
    private bool settingsMenuEnabled = false;
    private bool creditsMenuEnabled = true;
    private bool difficultyMenuEnabled = false;

    private void Start()
    {
        Screen.SetResolution(1920, 1080, true);
    }


    private void Update()
    {
        if (mainMenuEnabled == true)
        {
            if (mainMenu.alpha < 1)
            {
                mainMenu.alpha += Time.deltaTime;
            }
        }
        else if (mainMenuEnabled == false)
        {
            if (mainMenu.alpha > 0)
            {
                mainMenu.alpha -= Time.deltaTime;
            }
        }


        if (settingsMenuEnabled == true)
        {
            if (settingsMenu.alpha < 1)
            {
                settingsMenu.alpha += Time.deltaTime;
            }
        }
        else if (settingsMenuEnabled == false)
        {
            if (settingsMenu.alpha > 0)
            {
                settingsMenu.alpha -= Time.deltaTime;
            }
        }


        if (creditsMenuEnabled == true)
        {
            if(creditsMenu.alpha < 1)
            {
                creditsMenu.alpha += Time.deltaTime;
            }
        }
        else if (creditsMenuEnabled == false)
        {
            if (creditsMenu.alpha > 0)
            {
                creditsMenu.alpha -= Time.deltaTime;
            }
        }

        if (difficultyMenuEnabled == true)
        {
            if (difficultyMenu.alpha < 1)
            {
                difficultyMenu.alpha += Time.deltaTime;
            }
        }
        else if(difficultyMenuEnabled == false)
        {
            if (difficultyMenu.alpha > 0)
            {
                difficultyMenu.alpha -= Time.deltaTime;
            }
        }

        if (creditsMenuEnabled == true)
        {
            if( creditsMenu.alpha < 1)
            {
                creditsMenu.alpha += Time.deltaTime;
            }
        }
        if (creditsMenuEnabled == false)
        {
            if (creditsMenu.alpha > 0)
            {
                creditsMenu.alpha -= Time.deltaTime;
            }
        }
        if (creditsMenuEnabled == true && Input.GetKey(KeyCode.Escape))
        {
            creditsMenuEnabled = false;
            menu.SetActive(true);
            mainMenuEnabled = true;
        }
    }


    public void startClick()
    {
        mainMenuEnabled = false;
        StartCoroutine(startTempo());
    }

    public void backMenu()
    {
        difficulty.SetActive(true);
        difficultyMenuEnabled = false;
        StartCoroutine(backMenuTempo());
    }

    public void optionClick()
    {
        mainMenuEnabled = false;
        StartCoroutine(optionClickTempo());
    }

    public void optionBack()
    {
        menu.SetActive(true);
        settingsMenuEnabled = false;
        StartCoroutine(optionBackTempo());
    }

    public void creditClick()
    {
        mainMenuEnabled = false;
        StartCoroutine(creditClickTempo());
    }

    public void exitCreditMenu()
    {
        creditsMenuEnabled = false;
    }

    public void exitClick()
    {
        Application.Quit();
    }

    IEnumerator startTempo()
    {
        yield return new WaitForSeconds(tempo);
        difficulty.SetActive(true);
        difficultyMenuEnabled = true;
        yield return new WaitForSeconds(tempoDesactivation);
        menu.SetActive(false);
    }

    IEnumerator backMenuTempo()
    {
        yield return new WaitForSeconds(tempo);
        difficulty.SetActive(false);
        menu.SetActive(true);
        mainMenuEnabled = true;
    }

    IEnumerator optionClickTempo()
    {
        yield return new WaitForSeconds(tempo);
        menu.SetActive(false);
        setting.SetActive(true);
        settingsMenuEnabled = true;
    }

    IEnumerator optionBackTempo()
    {
        menu.SetActive(true);
        yield return new WaitForSeconds(tempo);
        setting.SetActive(false);
        mainMenuEnabled = true;
    }

    IEnumerator creditClickTempo()
    {
        yield return new WaitForSeconds(tempo);
        menu.SetActive(false);
        creditsMenuEnabled = true;
    }
}
