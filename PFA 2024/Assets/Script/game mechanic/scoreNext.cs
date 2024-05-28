using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class scoreNext : MonoBehaviour
{
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private CharacterController character;
    [SerializeField] private float objectif_1;
    [SerializeField] private float objectif_2;
    [SerializeField] private float objectif_3;
    private TextMeshProUGUI textScore;
    private float chrono;
    private float score = 0;
    private float scoreStockage = 0;
    private int difficulty;
    private int nbVie;
    private int multiplicator;
    private bool end;

    private void Awake()
    {
        score += PlayerPrefs.GetFloat("levelScore");
        textScore = GetComponent<TextMeshProUGUI>();
        nbVie = character.PointDeVie;
        score += nbVie * 100;
        difficulty = PlayerPrefs.GetInt("difficulty");
        if (difficulty == 1)
        {
            multiplicator = 115;
            score += 500;
        }
        else if (difficulty == 6)
        {
            multiplicator = 85;
        }
        else
        {
            multiplicator = 100;
            score += 200;
        }

        if (chrono >= objectif_1)
        {
            score += 275;
        }

        else if (chrono >= objectif_2)
        {
            score += 250;
        }

        else if (chrono >= objectif_3)
        {
            score += 225;
        }

        else
        {
            score += 100;
        }
        score = score * multiplicator;
    }

    private void Start()
    {
        textScore.SetText("Score : " + score);
        scoreStockage = score + PlayerPrefs.GetFloat("score");
        PlayerPrefs.SetFloat("score",scoreStockage);
        EventSystem.current.SetSelectedGameObject(nextLevel);
    }
}
