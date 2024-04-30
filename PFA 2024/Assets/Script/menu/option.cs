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
    [SerializeField] private CanvasGroup settingsMenu;
    [SerializeField] private CanvasGroup creditsMenu;
    [Header("Settings Menu")]
    [SerializeField] private CanvasGroup soundMenu;
    [SerializeField] private CanvasGroup controleMenu;
    [Header("Difficulty Menu")]
    [SerializeField] private CanvasGroup difficultyMenu;

    private float tempo = 0.9f;
    private bool mainMenuEnabled = true;
    private bool settingsMenuEnabled = false;
    private bool creditsMenuEnabled = true;
    private bool difficultyMenuEnabled = false;


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
        mainMenuEnabled = true;
        StartCoroutine(backMenuTempo());
    }

    public void optionClick()
    {
        mainMenuEnabled = false;
        StartCoroutine(optionClickTempo());
    }

    public void optionBack()
    {
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
        difficultyMenuEnabled = true;
    }

    IEnumerator backMenuTempo()
    {
        yield return new WaitForSeconds(tempo);
        difficultyMenuEnabled = false;
    }

    IEnumerator optionClickTempo()
    {
        yield return new WaitForSeconds(tempo);
        settingsMenuEnabled = true;
    }

    IEnumerator optionBackTempo()
    {
        yield return new WaitForSeconds(tempo);
        mainMenuEnabled = true;
    }

    IEnumerator creditClickTempo()
    {
        yield return new WaitForSeconds(tempo);
        creditsMenuEnabled = true;
    }
}
