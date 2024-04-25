using System;
using System.Threading;
using UnityEngine;
public class CharacterController : MonoBehaviour
{
    private Quaternion actualRotation;
    private Quaternion finalset;
    private Quaternion oldset;
    private Vector3 oldPosition;
    private Vector3 stockOldSet;
    private Vector3 endPosition;
    private Vector3 currentPosition;
    private int vie = 3;
    private float inputTime =4f;
    private float moveTime = 1.5f;
    private float rotationSpeed = 7.5f;
    private float elapsedTime;
    private float rotationTime;

    [Header("Configuration Touche")]
    [SerializeField] private KeyCode turnLeft;
    [SerializeField] private KeyCode turnRight;
    [Header("Avance")]
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode forwardSecond;
    [SerializeField] private KeyCode backward;
    [Header("Configuration")]
    [SerializeField] private GameObject spawnReset;
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
        if ((Input.GetKey(forward) || Input.GetKey(forwardSecond)) && jumpEnable == true && transform.position.x <5)
        {
            oldPosition = transform.position;
            elapsedTime = 0;
            endPosition = transform.position;
            endPosition += transform.forward * 1;
            jumpEnable = false;
        }

        else if (Input.GetKey(backward) && jumpEnable == true && transform.position.x > 5)
        {
            oldPosition = transform.position;
            elapsedTime = 0;
            endPosition = transform.position;
            endPosition -= transform.forward * 1;
            jumpEnable = false;
        }
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
        }
        if (Input.GetKey(turnRight) && jumpEnable == true)
        {
            elapsedTime = 0;
            oldset = transform.rotation;
            stockOldSet = oldset.eulerAngles;
            stockOldSet.y += 90;
            finalset = Quaternion.Euler(stockOldSet);
            jumpEnable = false;
        }
        elapsedTime += Time.fixedDeltaTime;
        float rotationComplete = elapsedTime / rotationSpeed;
        transform.rotation=Quaternion.Slerp(actualRotation, finalset, rotationComplete);

        if (elapsedTime > inputTime)
        {
            jumpEnable = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        Debug.Log(other.tag);
        if (other.tag == "Arbre")
        {
            endPosition = oldPosition ;
        }
        else if (other.tag == "vehicle")
        {
            vie -= 1;
            if (vie == 0)
            {
                stockOldSet.y = 0;
                finalset = Quaternion.Euler(stockOldSet);
                endPosition = spawnReset.transform.position;
                vie = 3;
            }
        }
        else if (other.tag == "eau")
        {
            endPosition = spawnReset.transform.position;
        }
    }

    private void avance()
    {
        if (actualRotation.eulerAngles.y != 0)
        {
            if (actualRotation.eulerAngles.y 
        }

    }
}
