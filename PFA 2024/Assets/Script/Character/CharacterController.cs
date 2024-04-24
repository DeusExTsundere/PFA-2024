using System;
using System.Threading;
using UnityEngine;
public class CharacterController : MonoBehaviour
{
    private Quaternion actualRotation;
    private Quaternion finalset;
    private Quaternion oldset;
    private Vector3 stockOldSet;
    private Vector3 endPosition;
    private Vector3 currentPosition;
    private float inputTime;
    private float moveTime = 1.5f;
    private float rotationSpeed = 7.5f;
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
    }

    private void Update()
    {
        currentPosition = transform.position;
        if (Input.GetKey(forward) && jumpEnable == true)
        {
            elapsedTime = 0;
            endPosition = transform.position;
            endPosition.z += 1;
            jumpEnable = false;
            inputTime = 3f;
        }

        if (Input.GetKey(backward) && jumpEnable == true)
        {
            elapsedTime = 0;
            endPosition = transform.position;
            endPosition.z -= 1;
            jumpEnable = false;
            inputTime = 3f;
        }

        elapsedTime += Time.fixedDeltaTime;
        float percentageComplete = elapsedTime / moveTime;
        transform.position = Vector3.Lerp(currentPosition, endPosition, percentageComplete);



        actualRotation = transform.rotation;
        if (Input.GetKey(turnLeft) && jumpEnable == true)
        {
            elapsedTime = 0;
            oldset = transform.rotation;
            stockOldSet = oldset.eulerAngles;
            stockOldSet.y -= 90;
            finalset = Quaternion.Euler(stockOldSet);
            jumpEnable = false;
            inputTime = rotationSpeed;
        }
        if (Input.GetKey(turnRight) && jumpEnable == true)
        {
            elapsedTime = 0;
            oldset = transform.rotation;
            stockOldSet = oldset.eulerAngles;
            stockOldSet.y += 90;
            finalset = Quaternion.Euler(stockOldSet);
            jumpEnable = false;
            inputTime = rotationSpeed;
        }
        elapsedTime += Time.fixedDeltaTime;
        float rotationComplete = elapsedTime / rotationSpeed;
        transform.rotation=Quaternion.Slerp(actualRotation, finalset, rotationComplete);

        if (elapsedTime > inputTime)
        {
            jumpEnable = true;
        }
    }
}
