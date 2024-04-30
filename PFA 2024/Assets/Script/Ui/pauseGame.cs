using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class pauseGame : MonoBehaviour
{
    private float complete;
    private float transition = 1.5f;
    private float elapsedTime;
    private RectTransform pause;
    private RectTransform unPause;
    private RectTransform actualState;
    private bool enablePause = false;
    void Start()
    {
        actualState = GetComponent<RectTransform>();
        unPause = actualState;
        pause.localPosition = new Vector3(-469, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        actualState = GetComponent<RectTransform>();
        elapsedTime += Time.deltaTime;
        if (Input.GetKey(KeyCode.Escape) && enablePause == false)
        {
            enablePause = true;
            actualState = pause;
            elapsedTime = 0;
        }
        else if (Input.GetKey(KeyCode.Escape) && enablePause == true)
        {
            enablePause = false;
            actualState = unPause;
            elapsedTime = 0;
        }
        complete = elapsedTime/transition;
        GetComponent<RectTransform>().localPosition = Vector3.Lerp(GetComponent<RectTransform>().localPosition, actualState,complete);

    }
}
