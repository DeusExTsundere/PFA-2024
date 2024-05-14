using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    [SerializeField, Range(0, 3)] private int damage = 1;
    public int lifeMinus { get { return damage; } }

    [SerializeField] private bool respawn = true;
    public bool spawn { get { return respawn; } }
}
