using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class input : MonoBehaviour
{
    private InputPlayer inputPlayer;
    private InputAction move;

    private void Awake()
    {
        inputPlayer = new InputPlayer();
    }

    private void Update()
    {
      
    }
}
