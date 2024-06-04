using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerObject : MonoBehaviour
{
    private Vector3 vector3;
    public Vector3 center { get { return vector3; } }
    void Start()
    {
        vector3 = transform.position;
        vector3.y = 0.505f;
    }
}
