using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voiture : MonoBehaviour
{
    [SerializeField] private int speed = 2;
    private bool coteSpawn;

    private void Start()
    {
        if (transform.position.x > 0)
        {
            coteSpawn = true;            
        }
        if (coteSpawn == true)
        {
            speed = -speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
