using System;
using System.Threading;
using UnityEngine;
public class characterController : MonoBehaviour
{

    private Quaternion finalset;
    private Quaternion oldset;
    private Vector3 endPosition;
    private Vector3 startPosition;
    private float moveTime = 0.5f;
    private float rotationSpeed = 1.5f;
    private float elapsedTime;
    private float rotationTime;

    [Header("Configuration Touche")]
    [SerializeField] private KeyCode turnLeft;
    [SerializeField] private KeyCode turnRight;
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode backward;
    private Rigidbody rb;
    private bool jumpEnable = true;
    private void Start()
    {
        endPosition = transform.position;
        finalset = transform.rotation;
        Debug.Log(finalset);
    }

    private void FixedUpdate()
    {
        startPosition = transform.position;
        if (Input.GetKey(forward) && jumpEnable == true)
        {
            elapsedTime = 0;
            endPosition = transform.position;
            endPosition.z += 1;
            jumpEnable = false;
        }

        if(Input.GetKey(backward) && jumpEnable == true)
        {
            elapsedTime = 0;
            endPosition = transform.position;
            endPosition.z -= 1;
            jumpEnable = false;
        }

        elapsedTime += Time.fixedDeltaTime;
        float percentageComplete = elapsedTime/moveTime;
        transform.position = Vector3.Lerp(startPosition,endPosition, percentageComplete);

        if (elapsedTime > 0.5f)
        {
            jumpEnable = true;
        }
        oldset = transform.rotation;
        if (Input.GetKey (turnLeft) && elapsedTime > rotationSpeed)
        {
            finalset = transform.rotation;
            elapsedTime = 0;
            finalset.y += 45;
        }

        if (Input.GetKey (turnRight) && elapsedTime > rotationSpeed)
        {
            finalset = transform.rotation;
            elapsedTime = 0;
            finalset.y -= 45;
        }
        elapsedTime += Time.fixedDeltaTime;
        float rotationTime = elapsedTime / moveTime;
        transform.rotation = Quaternion.Lerp(oldset,finalset, rotationTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        jumpEnable = true;
    }
}
