using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSelect : MonoBehaviour
{
    private int difficulty;
    [SerializeField] private string gameStart;

    public int Level {get { return difficulty;} }

    public void easy()
    {
        difficulty = 6;
        PlayerPrefs.SetInt("difficulty",difficulty);
        SceneManager.LoadScene(gameStart);
    }

    public void normal()
    {
        difficulty = 3;
        PlayerPrefs.SetInt("difficulty", difficulty);
        SceneManager.LoadScene(gameStart);
    }

    public void hard() 
    { 
        difficulty = 1;
        PlayerPrefs.SetInt("difficulty", difficulty);
        SceneManager.LoadScene(gameStart);
    }

}
