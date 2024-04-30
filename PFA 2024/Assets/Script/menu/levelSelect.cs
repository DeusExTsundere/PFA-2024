using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelect : MonoBehaviour
{
    private int difficulty;

    public int Level {get { return difficulty;} }

    public void easy()
    {
        difficulty = 1;
    }

    public void normal()
    {
        difficulty = 3;
    }

    public void hard() 
    { 
        difficulty = 6;
    }

}
