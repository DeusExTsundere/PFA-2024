using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.TextCore.Text;

public class lastLevel : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    [SerializeField] private GameObject actualScoring;
    private float score = 0;
    private float top;
    private int difficulty;
    private int nbVie;
    private int multiplicator;
    private TextMeshProUGUI highscore;

    private void Awake()
    {
        highscore = GetComponent<TextMeshProUGUI>();
        score += PlayerPrefs.GetFloat("levelChrono");
        nbVie = character.PointDeVie;
        score += nbVie * 50;
        difficulty = PlayerPrefs.GetInt("difficulty");
        if (difficulty == 1)
        {
            multiplicator = 115;
            score += 250;
        }
        else if (difficulty == 6)
        {
            multiplicator = 85;
        }
        else
        {
            multiplicator = 100;
            score += 150;
        }

        score = score * multiplicator;
    }
    void Start()
    {
        if (PlayerPrefs.GetFloat("score") > top)
        {
            StartCoroutine(newHighscore());
        }
        else
        {
            highscore.SetText("Actual Highscore :" + PlayerPrefs.GetFloat("score"));
            actualScoring.SetActive(true);
        }
    }

    IEnumerator newHighscore() 
    {
        highscore.SetText("Actual Higscore :" + top);
        yield return new WaitForSeconds(10);
        highscore.SetText("New Higscore :" + PlayerPrefs.GetFloat("score"));
        top = PlayerPrefs.GetFloat("score");
    }


}
