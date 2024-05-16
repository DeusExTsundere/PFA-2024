using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calculScore : MonoBehaviour
{
    [SerializeField]private CharacterController character;
    private int score;
    private int difficulty;
    private int nbVie;
    private float multiplicator;

    void Start()
    {
        difficulty = PlayerPrefs.GetInt("difficulty");
        if (difficulty == 1)
        {
            multiplicator = 1.15f;
        }
        else if (difficulty == 6)
        {
            multiplicator = 0.85f;
        }
        else
        {
            multiplicator = 1;
        }
        nbVie = character.PointDeVie;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        nbVie = character.PointDeVie;
    }
    private void OnTriggerExit(Collider other)
    {
        nbVie = character.PointDeVie;
    }
}
