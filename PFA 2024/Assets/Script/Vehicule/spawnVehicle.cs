using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnVehicle : MonoBehaviour
{
    [SerializeField]private GameObject voiture ;
    private float timer = 5f;
    private float chrono;

    void Update()
    {
        chrono += Time.deltaTime;
        if ( chrono >= timer )
        {
            chrono = 0;
            Instantiate(voiture,transform.position, Quaternion.identity);
            timer = Random.Range(3 ,7.5f);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
