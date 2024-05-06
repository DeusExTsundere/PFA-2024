using System.Collections;
using System.Threading;
using UnityEngine;
public class CharacterController : MonoBehaviour
{
    private Vector3 respawn;
    private Quaternion actualRotation;
    private Quaternion finalset;
    private Quaternion oldset;
    private Vector3 oldPosition;
    private Vector3 stockOldSet;
    private Vector3 endPosition;
    private Vector3 currentPosition;
    private float moveTime = 1f;
    public float speed { get { return moveTime; } }
    private float rotationSpeed = 1f;
    private float elapsedTime;
    private bool isAlive = true;
    private bool jumpEnable = true;
    private bool movementEnable=true;
    private bool resetPosition = false;

    private int vie = 3;
    public int PointDeVie { get { return vie; } }

    [Header("Configuratin Menu")]
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject menuFailed;
    [Header("Configuration Touche")]
    [SerializeField] private KeyCode turnLeft;
    [SerializeField] private KeyCode turnRight;
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode backward;
    [Header("Configuration")]
    [SerializeField] private Animator animator;
    [SerializeField] private float distanceSaut = 1.5f;
    [SerializeField] private float hauteurSaut = 1f;
    private void Start()
    {
        respawn = transform.position;
        endPosition = transform.position;
        finalset = transform.rotation;
    }

    private void Update()
    {
        currentPosition = transform.position;
        if (Input.GetKey(forward) && jumpEnable == true && (endPosition.z < 35 || currentPosition.z < 35))
        {
            moveForward();
        }

        else if (Input.GetKey(backward) && jumpEnable == true && (endPosition.z > -5 || currentPosition.z > -5))
        {
            moveBack();
        }
        float percentageComplete = elapsedTime / moveTime;

        actualRotation = transform.rotation;
        if (Input.GetKey(turnLeft) && jumpEnable == true && (endPosition.x > -5 || currentPosition.x > -5))
        {
            moveLeft();
        }
        if (Input.GetKey(turnRight) && jumpEnable == true  && (endPosition.x < 5 || currentPosition.x < 5 ))
        {
            moveRight();
        }
        elapsedTime += Time.fixedDeltaTime;
        float rotationComplete = elapsedTime / rotationSpeed;

        if ((currentPosition != endPosition && movementEnable == true) || resetPosition == true)
        {
            if (isAlive == true)
            {
                transform.position = Vector3.Lerp(currentPosition, endPosition, percentageComplete);
            }

        }
        if (currentPosition == endPosition)
        {
            movementEnable = false;
            resetPosition = false;
        }
        transform.rotation = Quaternion.Slerp(actualRotation, finalset, rotationComplete);

        if (vie == 0)
        {
            isAlive = false;
            ui.SetActive(false);
            menuFailed.SetActive(true);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arbre")
        {
            endPosition = oldPosition ;
        }
        else if (other.tag == "vehicle")
        {
            resetPosition = true;
            endPosition = respawn;
            vie -= 1;
        }
        else if (other.tag == "eau")
        {
            resetPosition = true;
            vie -= 1;
            endPosition = respawn;
        }
        else if (other.tag == "checkpoint")
        {
            respawn = other.transform.position;
            respawn.y += 0.5f ;
        }
        else if (other.tag == "Sol")
        {
            jumpEnable = true;
        }
        else if (other.tag == "platform")
        {
            jumpEnable = true;
            gameObject.transform.SetParent(other.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Sol")
        {
            jumpEnable=true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.transform.SetParent(null);
    }

    private void moveForward()
    {
        if (actualRotation.eulerAngles.y != 0)
        {
            if (actualRotation.eulerAngles.y == 90)
            {
                elapsedTime = 0;
                oldset = transform.rotation;
                stockOldSet = oldset.eulerAngles;
                stockOldSet.y = 0;
                finalset = Quaternion.Euler(stockOldSet);
                jumpEnable = false;
            }

            else if (actualRotation.eulerAngles.y == 180 )
            {
                int rotaRandom = Random.Range(0,2);
                if ( rotaRandom == 0 )
                {
                    elapsedTime = 0;
                    oldset = transform.rotation;
                    stockOldSet = oldset.eulerAngles;
                    stockOldSet.y = 90;
                    finalset = Quaternion.Euler(stockOldSet);
                    jumpEnable = false;
                }

                else if (rotaRandom > 0) 
                {
                    elapsedTime = 0;
                    oldset = transform.rotation;
                    stockOldSet = oldset.eulerAngles;
                    stockOldSet.y = 270;
                    finalset = Quaternion.Euler(stockOldSet);
                    jumpEnable = false;
                }

            }
            else if (actualRotation.eulerAngles.y == 270)
            {
                elapsedTime = 0;
                oldset = transform.rotation;
                stockOldSet = oldset.eulerAngles;
                stockOldSet.y = 0;
                finalset = Quaternion.Euler(stockOldSet);
                jumpEnable = false;
            }
        }
        else if (actualRotation.eulerAngles.y == 0)
        {
            movementEnable = true;
            oldPosition = transform.position;
            elapsedTime = 0;
            endPosition = transform.position;
            endPosition += transform.forward * distanceSaut;
            endPosition += transform.up * hauteurSaut;
            jumpEnable = false;
            animator.Play("saut");
        }

    }

    private void moveBack()
    {
        if (actualRotation.eulerAngles.y != 180)
        {
            if (actualRotation.eulerAngles.y == 90)
            {
                elapsedTime = 0;
                oldset = transform.rotation;
                stockOldSet = oldset.eulerAngles;
                stockOldSet.y = 180;
                finalset = Quaternion.Euler(stockOldSet);
                jumpEnable = false;
            }

            else if (actualRotation.eulerAngles.y == 0)
            {
                int rotaRandom = Random.Range(0, 2);
                if (rotaRandom == 0)
                {
                    elapsedTime = 0;
                    oldset = transform.rotation;
                    stockOldSet = oldset.eulerAngles;
                    stockOldSet.y = 90;
                    finalset = Quaternion.Euler(stockOldSet);
                    jumpEnable = false;
                }

                else if (rotaRandom > 0)
                {
                    elapsedTime = 0;
                    oldset = transform.rotation;
                    stockOldSet = oldset.eulerAngles;
                    stockOldSet.y = 270;
                    finalset = Quaternion.Euler(stockOldSet);
                    jumpEnable = false;
                }

            }

            else if (actualRotation.eulerAngles.y == 270)
            {
                elapsedTime = 0;
                oldset = transform.rotation;
                stockOldSet = oldset.eulerAngles;
                stockOldSet.y = 180;
                finalset = Quaternion.Euler(stockOldSet);
                jumpEnable = false;
            }
        }
        else if (actualRotation.eulerAngles.y == 180)
        {
            movementEnable = true;
            oldPosition = transform.position;
            elapsedTime = 0;
            endPosition = transform.position;
            endPosition += transform.forward * distanceSaut;
            endPosition += transform.up * hauteurSaut;
            jumpEnable = false;
            animator.Play("saut");
        }
    }

    private void moveRight()
    {
        if (actualRotation.eulerAngles.y != 90)
        {
            if (actualRotation.eulerAngles.y == 180)
            {
                elapsedTime = 0;
                oldset = transform.rotation;
                stockOldSet = oldset.eulerAngles;
                stockOldSet.y = 90;
                finalset = Quaternion.Euler(stockOldSet);
                jumpEnable = false;
            }


            else if (actualRotation.eulerAngles.y == 270)
            {
                int rotaRandom = Random.Range(0, 2);
                if (rotaRandom == 0)
                {
                    elapsedTime = 0;
                    oldset = transform.rotation;
                    stockOldSet = oldset.eulerAngles;
                    stockOldSet.y = 0;
                    finalset = Quaternion.Euler(stockOldSet);
                    jumpEnable = false;
                }

                else if (rotaRandom > 0)
                {
                    elapsedTime = 0;
                    oldset = transform.rotation;
                    stockOldSet = oldset.eulerAngles;
                    stockOldSet.y = 180;
                    finalset = Quaternion.Euler(stockOldSet);
                    jumpEnable = false;
                }
            }
            else if (actualRotation.eulerAngles.y == 0)
            {
                elapsedTime = 0;
                oldset = transform.rotation;
                stockOldSet = oldset.eulerAngles;
                stockOldSet.y = 90;
                finalset = Quaternion.Euler(stockOldSet);
                jumpEnable = false;
            }

        }
        else if (actualRotation.eulerAngles.y == 90)
        {
            movementEnable = true;
            oldPosition = transform.position;
            elapsedTime = 0;
            endPosition = transform.position;
            endPosition += transform.forward * distanceSaut;
            endPosition += transform.up * hauteurSaut;
            jumpEnable = false;
            animator.Play("saut");
        }
    }

    private void moveLeft()
    {
        if (actualRotation.eulerAngles.y != 270)
        {
            if (actualRotation.eulerAngles.y == 0)
            {
                elapsedTime = 0;
                oldset = transform.rotation;
                stockOldSet = oldset.eulerAngles;
                stockOldSet.y = 270;
                finalset = Quaternion.Euler(stockOldSet);
                jumpEnable = false;
            }

            else if (actualRotation.eulerAngles.y == 180)
            {
                elapsedTime = 0;
                oldset = transform.rotation;
                stockOldSet = oldset.eulerAngles;
                stockOldSet.y = 270;
                finalset = Quaternion.Euler(stockOldSet);
                jumpEnable = false;
            }

            else if (actualRotation.eulerAngles.y == 90)
            {
                int rotaRandom = Random.Range(0, 1);
                if (rotaRandom == 0)
                {
                    elapsedTime = 0;
                    oldset = transform.rotation;
                    stockOldSet = oldset.eulerAngles;
                    stockOldSet.y = 0;
                    finalset = Quaternion.Euler(stockOldSet);
                    jumpEnable = false;
                }

                else
                {
                    elapsedTime = 0;
                    oldset = transform.rotation;
                    stockOldSet = oldset.eulerAngles;
                    stockOldSet.y = 180;
                    finalset = Quaternion.Euler(stockOldSet);
                    jumpEnable = false;
                }

            }
        }
        else if (actualRotation.eulerAngles.y == 270)
        {
            movementEnable = true;
            oldPosition = transform.position;
            elapsedTime = 0;
            endPosition = transform.position;
            endPosition += transform.forward * distanceSaut;
            endPosition += transform.up * hauteurSaut;
            jumpEnable = false;
            animator.Play("saut");
        }
    }
}
