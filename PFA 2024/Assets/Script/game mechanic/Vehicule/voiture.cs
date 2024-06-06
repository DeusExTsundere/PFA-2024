using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voiture : MonoBehaviour
{
    [SerializeField,Range(1,10)] private int speed = 2;
    private Quaternion rotation;

    private void Start()
    {
        rotation = transform.rotation;

        if (transform.position.x > 0)
        {
            rotation.y = 180;
            transform.rotation = rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
