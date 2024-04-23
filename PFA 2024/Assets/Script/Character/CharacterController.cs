using System;
using UnityEngine;
public class CharacterController : MonoBehaviour
{
    //[Header("Equilibrage")]
   
    [Header("Configuration Touche")]
    [SerializeField] private KeyCode forward;

    private float speed = 0.02f;
    private float timeJump;
    private bool resetJump = true;
    public bool reset { get { return resetJump; } }
    public KeyCode jump { get { return forward; } }

    private void Start()
    {
        Debug.Log(transform.position.x);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(forward) && resetJump == true)
        {
            Vector3 _currentPosition = transform.position;
            while (timeJump < 1)
            {
                timeJump += 0.02f ;
                _currentPosition.x += speed;
                transform.position = _currentPosition;
            }
            resetJump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        resetJump = GetComponent<bool>();
        if (resetJump == true)
        {
            resetJump = false;
        }
    }

}
