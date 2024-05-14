using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroySpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] spawns;
    [SerializeField] private CharacterController character;

    void FixedUpdate()
    {
        if (character.PointDeVie < 1)
        {
            for (int i = 0; i < spawns.Length; i++)
            {
                Destroy(spawns[i]);
            }
        }
    }
}
