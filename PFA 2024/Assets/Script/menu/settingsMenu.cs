using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject soundSettings;
    [SerializeField] private GameObject controlSettings;
    public void SoundClick()
    {
        soundSettings.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SettingsClick()
    {
        controlSettings.SetActive(true);
        gameObject.SetActive(false);
    }
}
