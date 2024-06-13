using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class option : MonoBehaviour, ICancelHandler
{
    [Header("Main Menu")]
    [SerializeField] private CanvasGroup mainMenu;
    [SerializeField] private GameObject menu;
    [SerializeField] private CanvasGroup settingsMenu;
    [SerializeField] private GameObject setting;
    [SerializeField] private CanvasGroup creditsMenu;
    [Header("Settings Menu")]
    [SerializeField] private GameObject soundSettings;
    [SerializeField] private CanvasGroup soundMenu;
    [Header("Difficulty Menu")]
    [SerializeField] private CanvasGroup difficultyMenu;
    [SerializeField] private GameObject difficulty;
    [Header("Paramétrage")]
    [SerializeField] private GameObject defaultDifficulty;
    [SerializeField] private GameObject defaultMenuButton;
    [SerializeField] private GameObject optionButton;
    [SerializeField] private GameObject mainSoundBar;

    private float tempo = 0.9f;
    private float tempoDesactivation = 1f;
    private bool mainMenuEnabled = true;
    private bool settingsMenuEnabled = false;
    private bool creditsMenuEnabled = false;
    private bool difficultyMenuEnabled = false;
    private bool soundMenuEnabled = false;

    public void OnCancel(BaseEventData eventData)
    {
        mainMenuEnabled = true;
        EventSystem.current.SetSelectedGameObject(defaultMenuButton);
    }

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
        else if (settingsMenuEnabled == false  && soundMenuEnabled == false)
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

        if (soundMenuEnabled)
        {
            if (soundMenu.alpha < 1)
            {
                soundMenu.alpha += Time.deltaTime;
                settingsMenu.alpha -= Time.deltaTime; 
            }
        }
        else if (soundMenuEnabled == false)
        {
            if (soundMenu.alpha > 0)
            {

                soundMenu.alpha -= Time.deltaTime;
            }
        }
    }


    public void StartClick()
    {
        mainMenuEnabled = false;
        StartCoroutine(StartTempo());
        EventSystem.current.SetSelectedGameObject(defaultDifficulty);
    }

    public void BackMenu()
    {
        difficulty.SetActive(true);
        difficultyMenuEnabled = false;
        StartCoroutine(BackMenuTempo());
        EventSystem.current.SetSelectedGameObject(defaultMenuButton); ;
    }

    public void SoundClick()
    {
        soundMenuEnabled = !soundMenuEnabled;
        soundSettings.SetActive(true);
        StartCoroutine(SoundClickTempo());
        EventSystem.current.SetSelectedGameObject(mainSoundBar);
    }

    public void SoundExit()
    {
        soundMenuEnabled = !soundMenuEnabled;
        setting.SetActive(true);
        StartCoroutine(SoundExitTempo());
        EventSystem.current.SetSelectedGameObject(setting);
    }

    public void OptionClick()
    {
        mainMenuEnabled = false;
        StartCoroutine(OptionClickTempo());
        EventSystem.current.SetSelectedGameObject(optionButton);
    }

    public void OptionBack()
    {
        menu.SetActive(true);
        settingsMenuEnabled = false;
        StartCoroutine(OptionBackTempo());
        EventSystem.current.SetSelectedGameObject(defaultMenuButton);
    }

    public void CreditClick()
    {
        mainMenuEnabled = false;
        StartCoroutine(CreditClickTempo());
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void ExitCreditMenu(InputAction.CallbackContext context)
    {
        creditsMenuEnabled = false;
        EventSystem.current.SetSelectedGameObject(defaultMenuButton);
        mainMenuEnabled = true;
    }

    public void ExitClick()
    {
        Application.Quit();
    }

    IEnumerator StartTempo()
    {
        yield return new WaitForSeconds(tempo);
        difficulty.SetActive(true);
        difficultyMenuEnabled = true;
        yield return new WaitForSeconds(tempoDesactivation);
        menu.SetActive(false);
    }

    IEnumerator BackMenuTempo()
    {
        yield return new WaitForSeconds(tempo);
        difficulty.SetActive(false);
        menu.SetActive(true);
        mainMenuEnabled = true;
    }

    IEnumerator OptionClickTempo()
    {
        yield return new WaitForSeconds(tempo);
        menu.SetActive(false);
        setting.SetActive(true);
        settingsMenuEnabled = true;
    }

    IEnumerator OptionBackTempo()
    {
        menu.SetActive(true);
        yield return new WaitForSeconds(tempo);
        setting.SetActive(false);
        mainMenuEnabled = true;
    }

    IEnumerator CreditClickTempo()
    {
        yield return new WaitForSeconds(tempo);
        menu.SetActive(false);
        creditsMenuEnabled = true;
    }

    IEnumerator SoundClickTempo()
    {
        settingsMenuEnabled = false;
        yield return new WaitForSeconds(tempo);
        setting.SetActive(false);
    }

    IEnumerator SoundExitTempo()
    {
        settingsMenuEnabled = true;
        yield return new WaitForSeconds(tempo);
        setting.SetActive(true);
    }

}
