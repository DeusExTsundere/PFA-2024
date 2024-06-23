using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour 
{
    private Ray raycast;
    private Rigidbody rigid;
    private Vector3 respawn;
    private Vector3 newRotation;
    private Vector3 endPosition;
    private Vector3 oldPosition;
    private Vector3 oldDirection;
    private Vector2 direction;
    private bool isGrounded = false;
    private bool isAlive = true;
    private bool movementEnable = true;
    private bool paused = false;
    private bool finished = false;
    private bool forward = true;
    private bool onPlatform = false;
    public bool IsFinished { get { return finished; } }
    private float rotationComplete;
    private float moveComplete;
    private float elapsedTimeMove;
    private float elapsedTimeRotation;
    private float rotationSpeed;
    private float rotationWaits;
    private float timeWaits = 4;
    private int vie = 3;
    public int PointDeVie {get {return vie;}}
    private Direction currentDirection = Direction.none;
    private PlayerInput input;
    private RaycastHit hit;

    [Header("Configuratin Menu")]
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject menuFailed;
    [SerializeField] private GameObject victoryUI;
    [Header("Configuration")]
    [SerializeField] private AudioSource _jump;
    [SerializeField] private AudioSource _nenuphar;
    [SerializeField] private AudioSource _carDeath;
    [SerializeField] private AudioSource _waterDeath;
    [SerializeField, Range(0, 1.5f)] private float inputTime = 0.1f;
    [SerializeField] private int distanceSaut = 2;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private Animator animator;

    private const string ANIMATOR_JUMP_KEY = "Saut";

    private void Start()
    {
        input = GetComponent<PlayerInput>();
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

        if (finished == false)
        {
            if (!isGrounded)
            {
                movementEnable = false;
                rigid.isKinematic = false;
            }
            if (isAlive == true && movementEnable == true)
            {
                if (moveComplete < 1)
                {
                    transform.position = Vector3.Lerp(transform.position, endPosition, moveComplete);
                    movementEnable = false;
                }
                else if (moveComplete > 1)
                {
                    movementEnable = true;
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
            ui.SetActive(false);
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
        Debug.DrawRay(transform.position, transform.forward * 3 , Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3))
        {
            hit.transform.gameObject.TryGetComponent(out obstacle obstacle);
            if (obstacle.getTrough == false)
            {
                forward = false;
            }
        }
        else 
        {
            forward = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
        if (other.tag == "victoire")
        {
            finished = true;
        }
        else if (other.TryGetComponent(out trap trap))
        {
            vie -= trap.LifeMinus;
            if (trap.isWater == true)
            {
                _waterDeath.Play();
            }
            else
            {
                _carDeath.Play();
            }
            if (trap.Spawn == true)
            {
                elapsedTimeMove = 5;
                transform.position = respawn;
                elapsedTimeRotation = 0f;
            }
        }
        else if (other.TryGetComponent(out platform platform))
        {
            elapsedTimeRotation = 10f;
        }
        else if (other.TryGetComponent(out centerObject centerObject))
        {
            _nenuphar.Play();
            endPosition = centerObject.center;
        }
        if (isGrounded == true)
        {
            rigid.isKinematic = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        movementEnable = true;
        isGrounded = true ;
        if (other.TryGetComponent(out platform platform))
        {
            onPlatform = true ;
            distanceSaut = 1;
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
        else
        {
            onPlatform = false;
            distanceSaut = 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
        movementEnable = false;
    }

   

    public void MoveForward(InputAction.CallbackContext context)
    {
        if (context.started && movementEnable) 
        {
            movementEnable =false;
            oldPosition = transform.position;
            newRotation = transform.rotation.eulerAngles;
            if (currentDirection == Direction.front)
            {
                movementEnable |= movementEnable;
                TransformForward();
            }
            else
            {
                if (currentDirection == Direction.back)
                {
                    rotationSpeed = inputTime * 1.5f;
                    rotationWaits = (rotationSpeed/timeWaits);
                }
                else
                {
                    rotationSpeed = inputTime;
                    rotationWaits = (rotationSpeed/ timeWaits);
                }
                currentDirection = Direction.front;
                StartCoroutine(MoveRotationForward());
            }
            elapsedTimeRotation = 0f;
            newRotation.y = 0f;
        }
    }

    public void MoveBack(InputAction.CallbackContext context)
    {
        if (context.started && movementEnable)
        {
            oldPosition = transform.position;
            newRotation = transform.rotation.eulerAngles;
            if (currentDirection == Direction.back)
            {
                TransformBack();
            }
            else
            {
                if (currentDirection == Direction.front)
                {
                    rotationSpeed = inputTime * 1.5f;
                    rotationWaits = (rotationSpeed / timeWaits);
                }
                else
                {
                    rotationSpeed = inputTime;
                    rotationWaits = (rotationSpeed / timeWaits);
                }
                StartCoroutine(MoveRotationBack());
            }
            newRotation.y = 180f;
            elapsedTimeRotation = 0f;
            currentDirection = Direction.back;
        }
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.started && movementEnable)
        {
            oldPosition = transform.position;
            newRotation = transform.rotation.eulerAngles;
            if (currentDirection == Direction.right)
            {
                TransformRight();
            }
            else
            {
                if (currentDirection == Direction.left)
                {
                    rotationSpeed = inputTime * 1.5f;
                    rotationWaits = (rotationSpeed / timeWaits);
                }
                else
                {
                    rotationSpeed = inputTime;
                    rotationWaits = (rotationSpeed / timeWaits);
                }
                StartCoroutine(MoveRotationRight());
            }
            newRotation.y = 90f;
            elapsedTimeRotation = 0f;
            currentDirection = Direction.right;
        }
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.started && movementEnable)
        {
            oldPosition = transform.position;
            newRotation = transform.rotation.eulerAngles;
            if (currentDirection == Direction.left)
            {
                TransformLeft();
            }
            else
            {
                if (currentDirection == Direction.right)
                {
                    rotationSpeed = inputTime * 1.5f;
                    rotationWaits = (rotationSpeed / timeWaits);
                }
                else
                {
                    rotationSpeed = inputTime;
                    rotationWaits = (rotationSpeed / timeWaits);
                }
                StartCoroutine(MoveRotationLeft());
            }
            newRotation.y = 270f;
            elapsedTimeRotation = 0f;
            currentDirection = Direction.left;
        }

    }


    public void ForwardAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            TransformForward();
        }
    }

    public void Paused(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (finished == false)
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
                    EventSystem.current.SetSelectedGameObject(resumeButton);
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
    }

    public void ResumeClick()
    {
        paused = !paused;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        ui.SetActive(true);
    }

    public void CheatMod(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            vie = 999999999;
            isAlive = true;
            ui.SetActive(true);
            menuFailed.SetActive(false);
        }
    }
    private void TransformForward()
    {
        if (!forward)
        {
            return;
        }
        else if (onPlatform)
        {
            distanceSaut = 2;
        }
        elapsedTimeMove = 0f;
        endPosition = transform.position;
        endPosition.z += distanceSaut;
        StateTrigger();
    }

    private void TransformBack()
    {
        if (!forward)
        {
            return;
        }
        else if (onPlatform)
        {
            distanceSaut = 2;
        }
        elapsedTimeMove = 0f;
        endPosition = transform.position;
        endPosition.z -= distanceSaut;
        StateTrigger();
    }

    private void TransformRight()
    {
        if (!forward)
        {
            return;
        } 
        elapsedTimeMove = 0f;
        endPosition = transform.position;
        endPosition.x += distanceSaut;
        StateTrigger();
    }

    private void TransformLeft()
    {
        if (!forward)
        {
            return;
        }
        elapsedTimeMove = 0f;
        endPosition = transform.position;
        endPosition.x -= distanceSaut;
        StateTrigger();
    }


    public void StateTrigger()
    {
        animator?.SetTrigger(ANIMATOR_JUMP_KEY);
        _jump.Play();
    }

    IEnumerator MoveRotationForward()
    {
        yield return new WaitForSeconds(rotationWaits);
        TransformForward();
    }

    IEnumerator MoveRotationBack()
    {
        yield return new WaitForSeconds(rotationWaits);
        TransformBack();
    }

    IEnumerator MoveRotationRight()
    {
        yield return new WaitForSeconds(rotationWaits);
        TransformRight();
    }

    IEnumerator MoveRotationLeft()
    {
        yield return new WaitForSeconds(rotationWaits);
        TransformLeft();
    }
}

enum Direction
{
    front,
    back,
    left,
    right,
    none
}
