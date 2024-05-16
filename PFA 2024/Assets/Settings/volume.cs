using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volume : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider fxSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private AudioMixer master;
    [SerializeField] private GameObject settingsMenu;

    private float masterVolume;
    private float fxVolume;
    private float soundVolume;

    private void OnEnable()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        fxVolume = PlayerPrefs.GetFloat("fxVolume");
        soundVolume = PlayerPrefs.GetFloat("soundVolume");
        masterSlider.value = masterVolume;
        fxSlider.value = fxVolume;
        soundSlider.value = soundVolume;
    }

    private void Update()
    {
        master.SetFloat("Master", masterVolume);
        master.SetFloat("FXSound", fxVolume);
        master.SetFloat("Music" , soundVolume);
        masterVolume = masterSlider.value;
        fxVolume = fxSlider.value;
        soundVolume = soundSlider.value;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MasterVolume",masterVolume);
        PlayerPrefs.SetFloat("fxVolume",fxVolume);
        PlayerPrefs.SetFloat("soundVolume", soundVolume);   
    }

    public void Back()
    {
        settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
