using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLerp : MonoBehaviour
{
    private Vector3 endPosition = new Vector3 (4.5f, 1, 4.5f);
    private Vector3 startPositon;
    private float desiredDuration = 3f;
    private float elapsedTime;
    void Start()
    {
        startPositon = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;
        transform.position = Vector3.Lerp(startPositon,endPosition,percentageComplete);
    }
}
