using System;
using System.Threading;
using UnityEngine;
public class CharacterController : MonoBehaviour
{
    private Vector3 endPosition;
    private Vector3 startPosition;
    private float moveTime = 100f;
    private float elapsedTime;

    [Header("Configuration Touche")]
    [SerializeField] private KeyCode forward;

    public KeyCode jump { get { return forward; } }

    private int nbJump =1;
    public int countJump { get { return nbJump; } }

    private void Start()
    {
        endPosition = transform.position;
    }

    private void Update()
    {   
        startPosition = transform.position;
        if (Input.GetKey(forward) && nbJump > 0)
        {
            endPosition = transform.position;
            endPosition.z += 1;
            nbJump -= 1;
        }
        elapsedTime += Time.deltaTime;
        float percentageComplete= elapsedTime/moveTime;
        transform.position = Vector3.Lerp(startPosition,endPosition, percentageComplete);


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sol" && nbJump<=1)
        {
            nbJump = 1;
        }
    }
}
