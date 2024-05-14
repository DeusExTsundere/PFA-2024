using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour 
{
    private Vector3 respawn;
    private Vector3 newRotation;
    private Vector3 endPosition;
    private bool isAlive = true;
    private bool movementEnable = true;
    private float elapsedTime;
    private float rotationSpeed;
    private int vie = 3;
    public int PointDeVie {get {return vie;}}
    private direction currentDirection = direction.none;

    [Header("Configuratin Menu")]
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject menuFailed;
    [Header("Configuration Touche")]
    [SerializeField] private KeyCode turnLeft;
    [SerializeField] private KeyCode turnRight;
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode backward;
    [Header("Configuration")]
    [SerializeField, Range(0, 1.5f)] private float inputTime = 0.5f;
    [SerializeField] private float distanceSaut = 1.5f;
    private void Start()
    {
        vie = PlayerPrefs.GetInt("difficulty");
        respawn = transform.position;
        endPosition = transform.position;
    }

    private void Update()
    {

        if(movementEnable)
        {
            if (Input.GetKeyDown(forward))
            {
                moveForward();
            }
            else if (Input.GetKeyDown(backward))
            {
                moveBack();
            }
            else if (Input.GetKeyDown(turnRight))
            {
                moveRight();
            }
            else if (Input.GetKeyDown(turnLeft))
            {
                moveLeft();
            }
        }
        
        elapsedTime+= Time.deltaTime;
        float rotationComplete = elapsedTime / rotationSpeed;
        float percentageComplete = elapsedTime/inputTime;

        if (isAlive == true)
        {
            if (percentageComplete <= 1)
            {
                transform.position = Vector3.Lerp(transform.position, endPosition, percentageComplete);
            }
            else if (percentageComplete > 1)
            {
                movementEnable = true;
            }

            if (rotationComplete <= 1)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), rotationComplete);
            }
            else if (rotationComplete > 1)
            {
                movementEnable= true;
            }
        }
        

        if (vie <= 0)
        {
            isAlive = false;
            ui.SetActive(false);
            menuFailed.SetActive(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out obstacle obstacle))
        {
            if (obstacle.getTrough == false)
            {
                endPosition -= transform.forward * distanceSaut;
                elapsedTime = 0f;
            }
        }
        else if (other.TryGetComponent(out trap trap))
        {
            vie -= trap.lifeMinus;
            if (trap.spawn == true)
            {
                transform.position = respawn;
                elapsedTime = 0f;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out platform platform))
        {
            Vector3 platformTranslate = transform.position;
            if (platform.Side)
            {
                platformTranslate.x += platform.PlatformSpeed * Time.deltaTime;
            }
            else if (!platform.Side)
            {
                platformTranslate.x -= platform.PlatformSpeed * Time.deltaTime;
            }
            transform.position = platformTranslate;
        }
    }

    private void moveForward()
    {
        newRotation = transform.rotation.eulerAngles;
        if (currentDirection == direction.front) 
        {
            transformForward();
        }
        else
        {
           if (currentDirection == direction.back)
           {
                rotationSpeed = inputTime * 1.5f;
           }
           else 
           {
                rotationSpeed = inputTime;
           }
            currentDirection = direction.front;
        }
        elapsedTime=0f;
        newRotation.y = 0f;
    }

    private void moveBack()
    {
        newRotation = transform.rotation.eulerAngles;
        if (currentDirection == direction.back)
        {
            transformForward();
        }
        else
        {
            if (currentDirection == direction.front)
            {
                rotationSpeed = inputTime * 1.5f;
            }
            else
            {
                rotationSpeed = inputTime;
            }
        }
        newRotation.y = 180f;
        elapsedTime=0f;
        currentDirection = direction.back;
    }

    private void moveRight()
    {
        newRotation = transform.rotation.eulerAngles;
        if (currentDirection == direction.right)
        {
            transformForward();
        }
        else
        {
            if (currentDirection == direction.left)
            {
                rotationSpeed = inputTime * 1.5f;
            }
            else
            {
                rotationSpeed = inputTime;
            }
        }
        newRotation.y = 90f;
        elapsedTime=0f;
        currentDirection = direction.right;
    }

    private void moveLeft()
    {
        newRotation = transform.rotation.eulerAngles;
        if (currentDirection == direction.left)
        {
            transformForward();
        }
        else
        {
            if (currentDirection == direction.right)
            {
                rotationSpeed = inputTime * 1.5f;
            }
            else
            {
                rotationSpeed = inputTime;
            }
        }
        newRotation.y = 270f;
        elapsedTime=0f;
        currentDirection = direction.left;
    }

    private void transformForward()
    {
        elapsedTime = 0f;
        endPosition = transform.position;
        endPosition += transform.forward * distanceSaut;
    }
}

enum direction
{
    front,
    back,
    left,
    right,
    none
}