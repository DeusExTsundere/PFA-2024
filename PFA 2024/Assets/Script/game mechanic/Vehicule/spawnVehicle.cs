using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnVehicle : MonoBehaviour
{
    [SerializeField] private GameObject voiture ;
    [SerializeField, Range(1, 8)] private int spawnDelaiMax = 8;
    [SerializeField, Range(1, 8)] private int spawnDelaiMin = 1;
    private int voitureChoix;
    private bool spawnable = true;
    private float timer;
    private float chrono;

    void Update()
    {
        if (spawnable)
        {
            chrono += Time.deltaTime;
            if (chrono >= timer)
            {
                chrono = 0;
                Instantiate(voiture, transform.position, Quaternion.identity);
                timer = Random.Range(spawnDelaiMin,spawnDelaiMax);
                spawnable = false;
            }
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        spawnable = true;
    }
}
