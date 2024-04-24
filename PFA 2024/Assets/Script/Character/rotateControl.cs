using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateControl : MonoBehaviour
{
    private Quaternion endStockageRotation;
    private Quaternion startRotation;
    [SerializeField] private KeyCode turnLeft;
    [SerializeField] private KeyCode turnRight;
    private Quaternion newset;

    private void Start()
    {
        newset = transform.rotation;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        startRotation = transform.rotation;
         if (Input.GetKey(turnLeft))
        {
            endStockageRotation = transform.rotation;

        }
    }
}
