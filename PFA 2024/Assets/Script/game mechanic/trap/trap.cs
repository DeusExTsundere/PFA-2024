using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    [SerializeField] private bool water = false;
    public bool isWater { get { return water; } } 
    [SerializeField, Range(0, 6)] private int damage = 1;
    public int LifeMinus { get { return damage; } }

    [SerializeField] private bool respawn = true;
    public bool Spawn { get { return respawn; } }

    private void Start()
    {
        if (water)
        {
            damage = PlayerPrefs.GetInt("difficulty");
        }
    }
}
