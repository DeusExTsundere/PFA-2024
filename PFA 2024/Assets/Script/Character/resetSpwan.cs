using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetSpwan : MonoBehaviour
{
    [SerializeField] private GameObject spawn;
    private Transform respawn;
    private int pointDeVie = 3;

    private void Start()
    {
        respawn = spawn.transform;
    }

    private void Update()
    {
        if (pointDeVie == 0)
        {
            transform.position = respawn.transform.position;
            pointDeVie = 3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        pointDeVie -= 1;
        transform.position = respawn.transform.position;

        if (other.tag == "eau")
        {
            pointDeVie -= 1;
        }

        if (other.tag == "checkpoint")
        {
            respawn= other.GetComponent<Transform>();
        }
    }
}
