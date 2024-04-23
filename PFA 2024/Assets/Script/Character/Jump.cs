using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Jump : MonoBehaviour
{
    [SerializeField] private Animator jumpAnimator;
    private KeyCode jumpKeyCode;
    private bool jumpEnabled;
    [SerializeField] CharacterController characterController;
    // Start is called before the first frame update

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Start()
    {
        jumpKeyCode = characterController.jump;
        jumpEnabled = characterController.reset;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(jumpKeyCode) && jumpEnabled == true) 
        {
            Debug.Log("Jump");
            jumpAnimator.Play(0);
        }
    }
}
