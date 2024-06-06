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
    public bool IsFinished { get { return finished; } }
    private float rotationComplete;
    private float moveComplete;
    private float elapsedTimeMove;
    private float elapsedTimeRotation;
    private float rotationSpeed;
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
    [SerializeField, Range(0, 1.5f)] private float inputTime = 0.1f;
    [SerializeField] private int distanceSaut = 2;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GameObject resumeButton;
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
            if (trap.Spawn == true)
            {
                StartCoroutine(Death());
                elapsedTimeRotation = 0f;
            }
        }
        else if (other.TryGetComponent(out platform platform))
        {
            elapsedTimeRotation = 10f;

        }
        else if (other.TryGetComponent(out centerObject centerObject))
        {
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
        movementEnable = false;
    }

   

    public void MoveForward(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            movementEnable =false;
            oldPosition = transform.position;
            if ((endPosition.z + distanceSaut) >= 48)
            {
                return;
            }
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
            oldPosition = transform.position;
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
            oldPosition = transform.position;
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
            oldPosition = transform.position;
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


    public void ForwardAction(InputAction.CallbackContext context)
    {
        if ((((endPosition.x + distanceSaut) >= 7) && currentDirection == Direction.right)|| (((endPosition.x - distanceSaut) <= -7 && currentDirection == Direction.left)) || (((endPosition.z - distanceSaut) <= -5) && currentDirection == Direction.back) || !forward)
        {
            return;
        }
        else if (context.started)
        {
            oldPosition = transform.position;
            elapsedTimeMove = 0f;
            endPosition = transform.position;
            endPosition += transform.forward * distanceSaut;
        }
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
        elapsedTimeMove = 0f;
        endPosition = transform.position;
        endPosition += transform.forward * distanceSaut;
    }

    IEnumerator Death()
    {
        input.DeactivateInput();
        yield return new WaitForSeconds(0.75f);
        transform.position = respawn;
        input.ActivateInput();
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
