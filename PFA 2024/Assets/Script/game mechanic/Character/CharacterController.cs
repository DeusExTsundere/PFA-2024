using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour 
{
    private Rigidbody rigid;
    private Vector3 respawn;
    private Vector3 newRotation;
    private Vector3 endPosition;
    private Vector2 direction;
    private bool isGrounded = false;
    private bool isAlive = true;
    private bool movementEnable = true;
    private bool paused = false;
    private bool finished = false;
    public bool IsFinished { get { return finished; } }
    private float rotationComplete;
    private float moveComplete;
    private float elapsedTimeMove;
    private float elapsedTimeRotation;
    private float rotationSpeed;
    private int vie = 3;
    public int PointDeVie {get {return vie;}}
    private Direction currentDirection = Direction.none;

    [Header("Configuratin Menu")]
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject menuFailed;
    [SerializeField] private GameObject victoryUI;
    [Header("Configuration Touche")]
    [SerializeField] private KeyCode turnLeft;
    [SerializeField] private KeyCode turnRight;
    [SerializeField] private KeyCode forward;
    [SerializeField] private KeyCode backward;
    [Header("Configuration")]
    [SerializeField, Range(0, 1.5f)] private float inputTime = 0.5f;
    [SerializeField] private float distanceSaut = 1.5f;

    [SerializeField] private CharacterController characterController;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        vie = PlayerPrefs.GetInt("difficulty");
        respawn = transform.position;
        endPosition = transform.position;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        elapsedTimeMove += Time.deltaTime;
        elapsedTimeRotation += Time.deltaTime;
        rotationComplete = elapsedTimeRotation / rotationSpeed;
        moveComplete = elapsedTimeMove / inputTime;

        if (finished == false && paused == false)
        {
            pauseMenu.SetActive(false);
            ui.SetActive(true);
            if (isAlive == true)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                if (moveComplete <= 1)
                {
                    transform.position = Vector3.Lerp(transform.position, endPosition, moveComplete);
                }
                else if (moveComplete >= 1)
                {
                    movementEnable = true;
                    if (isGrounded == false)
                    {
                        rigid.isKinematic = false;
                    }
                }

                if (rotationComplete <= 1)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), rotationComplete);
                }
                else if (rotationComplete >= 1)
                {
                    movementEnable = true;
                }
            }
        }
        else if (finished == true)
        {
            victoryUI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (vie <= 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isAlive = false;
            ui.SetActive(false);
            menuFailed.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F1)) 
        {
            vie = 999999999;
            isAlive = true;
            ui.SetActive(true);
            menuFailed.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
        if (other.tag == "victoire")
        {
            finished = true;
        }
        if (other.TryGetComponent(out obstacle obstacle))
        {
            if (obstacle.getTrough == false)
            {
                endPosition -= transform.forward * distanceSaut;
                elapsedTimeRotation = 0f;
            }
        }
        else if (other.TryGetComponent(out trap trap))
        {
            vie -= trap.lifeMinus;
            if (trap.spawn == true)
            {
                StartCoroutine(Death());
                elapsedTimeRotation = 0f;
            }
        }
        else if (other.TryGetComponent(out platform platform))
        {
            elapsedTimeRotation = 10f;

        }
        if (isGrounded == true)
        {
            rigid.isKinematic = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        isGrounded = true ;
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

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }

    public void Paused(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isAlive)
            {
                paused = !paused;
            }
            if (paused == true)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                ui.SetActive(false);
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                ui.SetActive(true);

            }
        }
    }
   

    public void MoveForward(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            movementEnable = false;
            if ((endPosition.z + distanceSaut) >= 48)
            {
                return;
            }
            newRotation = transform.rotation.eulerAngles;
            if (currentDirection == Direction.front)
            {
                TransformForward();
            }
            else
            {
                if (currentDirection == Direction.back)
                {
                    rotationSpeed = inputTime * 1.5f;
                }
                else
                {
                    rotationSpeed = inputTime;
                }
                currentDirection = Direction.front;
            }
            elapsedTimeRotation = 0f;
            newRotation.y = 0f;
        }
    }

    public void MoveBack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            movementEnable = false;
            newRotation = transform.rotation.eulerAngles;
            if ((endPosition.z - distanceSaut) <= -5)
            {
                return;
            }
            if (currentDirection == Direction.back)
            {
                TransformForward();
            }
            else
            {
                if (currentDirection == Direction.front)
                {
                    rotationSpeed = inputTime * 1.5f;
                }
                else
                {
                    rotationSpeed = inputTime;
                }
            }
            newRotation.y = 180f;
            elapsedTimeRotation = 0f;
            currentDirection = Direction.back;
        }
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            movementEnable = false;
            newRotation = transform.rotation.eulerAngles;
            if ((endPosition.x + distanceSaut) >= 7)
            {
                return;
            }
            if (currentDirection == Direction.right)
            {
                TransformForward();
            }
            else
            {
                if (currentDirection == Direction.left)
                {
                    rotationSpeed = inputTime * 1.5f;
                }
                else
                {
                    rotationSpeed = inputTime;
                }
            }
            newRotation.y = 90f;
            elapsedTimeRotation = 0f;
            currentDirection = Direction.right;
        }
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            movementEnable = false;
            newRotation = transform.rotation.eulerAngles;
            if ((endPosition.x - distanceSaut) <= -7)
            {
                return;
            }
            if (currentDirection == Direction.left)
            {
                TransformForward();
            }
            else
            {
                if (currentDirection == Direction.right)
                {
                    rotationSpeed = inputTime * 1.5f;
                }
                else
                {
                    rotationSpeed = inputTime;
                }
            }
            newRotation.y = 270f;
            elapsedTimeRotation = 0f;
            currentDirection = Direction.left;
        }

    }

    private void TransformForward()
    {
        elapsedTimeMove = 0f;
        endPosition = transform.position;
        endPosition += transform.forward * distanceSaut;
    }

    public void ResumeClick()
    {
        paused = false;
        Time.timeScale = 1f;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.75f);
        transform.position = respawn;
    }

    //public void Move(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        direction = context.ReadValue<Vector2>();
    //        transform.position += new Vector3(direction.x,0,direction.y);

    //        //if (context.ReadValue<Vector2>().ba)
    //    }
    //}
}

enum Direction
{
    front,
    back,
    left,
    right,
    none
}
