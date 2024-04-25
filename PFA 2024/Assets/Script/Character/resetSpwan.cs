using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetSpwan : MonoBehaviour
{
    [SerializeField] private GameObject spawn;
    private int pointDeVie = 3;

    private void Update()
    {
        if (pointDeVie == 0)
        {
            transform.position = spawn.transform.position;
            pointDeVie = 3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        pointDeVie -= 1;
    }
}
