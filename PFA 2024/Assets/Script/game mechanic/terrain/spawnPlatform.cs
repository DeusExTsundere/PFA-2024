using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlatform : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    private Vector3 spawnPosition;
    private bool spawnEnable=true;
    private int randomPlatform=1;
    private float spawnTime;
    private float chrono;

    private void Start()
    {
        spawnPosition = transform.position;
        spawnPosition.y = -0.25f;
    }

    void Update()
    {
        if (spawnEnable == true)
        {
            chrono += Time.deltaTime;
            if (chrono >= spawnTime)
            {
                chrono = 0;
                Instantiate(platform,spawnPosition, Quaternion.identity);
                spawnTime = Random.Range(2.5f, 10);
                spawnEnable = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        spawnEnable=true;
    }
}
