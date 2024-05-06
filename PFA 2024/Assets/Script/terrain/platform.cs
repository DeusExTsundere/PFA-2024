using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    [SerializeField] private int speed = 2;
    private bool direction=false;
    public bool orientation {  get { return direction; } }
    private Quaternion rotation;
    void Start()
    {
        rotation = transform.rotation;

        if (transform.position.x > 0)
        {
            direction = true;
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
