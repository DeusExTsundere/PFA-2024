//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Timeline;
//using UnityEngine;

//public class pauseGame : MonoBehaviour
//{
//    private float elapsedTime;
//    [SerializeField, Range(0, 3)] private float transitionTime = 1;
//    private float percentageComplete;
//    private RectTransform RectTransform;
//    private RectTransform actualTransform;
//    private RectTransform expectedTransform;
//    private Vector2 pauseVector = new Vector2(0, 0);
//    private Vector2 unPauseVector = new Vector2(470, 0);
//    private RectTransform unPause;
//    private RectTransform pause;
//    private bool enablePause = false;

//    private void Start()
//    {
//        actualTransform = GetComponent<RectTransform>();
//        expectedTransform = pauseVector;
//    }

//    void Update()
//    {
//        elapsedTime += Time.deltaTime;
//        if (Input.GetKey(KeyCode.Escape) && enablePause == false)
//        {
//            actualTransform = GetComponent<RectTransform>();
//            elapsedTime = 0;
//            expectedTransform.anchoredPosition = pauseVector;
//        }
//        else if (Input.GetKey(KeyCode.Escape) && enablePause == true)
//        {
//            actualTransform = GetComponent<RectTransform>();
//            elapsedTime = 0;
//            expectedTransform.anchoredPosition = unPauseVector;
//        }
//        percentageComplete = elapsedTime / transitionTime;
//        actualTransform. = Vector2.Lerp(actualTransform, expectedTransform, percentageComplete);
//    }


//}
