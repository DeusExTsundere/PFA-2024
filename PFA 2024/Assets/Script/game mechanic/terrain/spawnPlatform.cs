using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] platform;
    private bool spawnEnable=true;
    private int randomPlatform=1;
    private float spawnTime;
    private float chrono;

    void Update()
    {
        if (spawnEnable == true)
        {
            chrono += Time.deltaTime;
            if (chrono >= spawnTime)
            {
                chrono = 0;
                Instantiate(platform[randomPlatform],transform.position, Quaternion.identity);
                spawnTime = Random.Range(2.5f, 10);
                spawnEnable = false;
                randomPlatform = Random.Range(0, platform.Length);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        spawnEnable=true;
    }
}
