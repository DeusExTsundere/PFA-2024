using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateControl : MonoBehaviour
{
    [SerializeField] private KeyCode turnLeft;
    [SerializeField] private KeyCode turnRight;
    private Quaternion newset;

    private void Start()
    {
        newset = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(turnRight))
        {
            for (int i = 0; i < 90; i++)
            {
                newset.y += 1;
                transform.rotation = newset;
            }
        }
        else if (Input.GetKey(turnLeft))
        {
            for (int i = 0; i < 90; i++)
            {
                newset.y -= 1;
                transform.rotation = newset;
            }
        }
    }
}
