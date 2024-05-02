using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSelect : MonoBehaviour
{
    private int difficulty;
    [SerializeField] private string gameStart;

    public int Level {get { return difficulty;} }

    public void easy()
    {
        difficulty = 1;
        SceneManager.LoadScene(gameStart);
    }

    public void normal()
    {
        difficulty = 3;
        SceneManager.LoadScene(gameStart);
    }

    public void hard() 
    { 
        difficulty = 6;
        SceneManager.LoadScene(gameStart);
    }

}
