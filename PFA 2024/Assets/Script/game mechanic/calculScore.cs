using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calculScore : MonoBehaviour
{
    [SerializeField]private CharacterController character;
    private int score;
    public int endScore { get { return score; } }
    private int difficulty;
    private int nbVie;
    private int multiplicator;
    private bool calcul = true;
    private bool end;

    private void Start()
    {
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
        nbVie = character.PointDeVie;
    }

    private void Update()
    {
        if (end == false)
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        nbVie = character.PointDeVie;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "victoire" && calcul == true)
        {
            end = character.IsFinished;
            nbVie = character.PointDeVie;
            score += nbVie * 100;
            score = score * multiplicator;
            calcul = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        nbVie = character.PointDeVie;
    }
}
