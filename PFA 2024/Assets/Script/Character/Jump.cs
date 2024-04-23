using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Jump : MonoBehaviour
{
    private Vector3 endPosition;
    private Vector3 startPosition;
    private float moveTime = 100f;
    private float elapsedTime;
    private KeyCode jumpKeyCode;
    private Rigidbody rb;
    private int jumpCount;
    private CharacterController player;
    private void Awake()
    {
        player = GetComponent<CharacterController>();
    }

    void Start()
    {
        endPosition = transform.position;
        jumpKeyCode = player.jump;
        int jumpCount = player.countJump;
    }

    private void Update()
    {
        int jumpCount = player.countJump;
        startPosition = transform.position;
        if (Input.GetKeyDown(jumpKeyCode) && jumpCount>0) 
        {
            endPosition = transform.position;
            endPosition.y += 1;
            jumpCount -= 1;
            while (transform.position != endPosition)
            {
                elapsedTime += Time.deltaTime;
                float percentageComplete = elapsedTime / moveTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);
            }
        }
    }

}
