using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voiture : MonoBehaviour
{
    [SerializeField,Range(1,10)] private int speed = 2;
    [SerializeField] private bool preSpawn=false;
    private Quaternion rotation;

    private void Start()
    {
        rotation = transform.rotation;

        if (preSpawn)
        {
            return;
        }

        if (transform.position.x > 0 )
        {
            rotation.y = 180;
            transform.rotation = rotation;
        }
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
