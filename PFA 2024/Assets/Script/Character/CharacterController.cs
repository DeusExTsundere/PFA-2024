using System.Collections;
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
    private float inputTime = 1.5f;
    private float moveTime = 1.3f;
    private float rotationSpeed = 7.5f;
    private float elapsedTime;
    private float rotationTime;
    private bool jumpEnable = true;

    [Header("Configuration Touche")]
    [SerializeField] private KeyCode turnLeft;
    [SerializeField] private KeyCode turnRight;
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode backward;
    [Header("Configuration")]
    [SerializeField] private GameObject spawnReset;
    [SerializeField] private Animator animator;
    private void Start()
    {
        endPosition = transform.position;
        finalset = transform.rotation;
    }

    private void Update()
    {
        currentPosition = transform.position;
        if (Input.GetKey(forward) && jumpEnable == true && endPosition.z < 35)
        {
            moveForward();
        }

        else if (Input.GetKey(backward) && jumpEnable == true && endPosition.z > -5)
        {
            moveBack();
        }
        float percentageComplete = elapsedTime / moveTime;
        transform.position = Vector3.Lerp(currentPosition, endPosition, percentageComplete);



        actualRotation = transform.rotation;
        if (Input.GetKey(turnLeft) && jumpEnable == true && endPosition.x > -5)
        {
            moveLeft();
        }
        if (Input.GetKey(turnRight) && jumpEnable == true  && endPosition.x < 5)
        {
            moveRight();
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
            oldPosition = transform.position;
            elapsedTime = 0;
            endPosition = transform.position;
            endPosition += transform.forward * 1;
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
            oldPosition = transform.position;
            elapsedTime = 0;
            endPosition = transform.position;
            endPosition += transform.forward * 1;
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
             oldPosition = transform.position;
             elapsedTime = 0;
             endPosition = transform.position;
             endPosition += transform.forward * 1;
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
            oldPosition = transform.position;
            elapsedTime = 0;
            endPosition = transform.position;
            endPosition += transform.forward * 1;
            jumpEnable = false;
            animator.Play("saut");
        }
    }
}
