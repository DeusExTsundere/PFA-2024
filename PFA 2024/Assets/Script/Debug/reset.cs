using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class reset : MonoBehaviour
{
    private string currentScene;
    private void Start()
    {
        currentScene =  SceneManager.GetActiveScene().name;
    }

    public void ResetLevel(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SceneManager.LoadScene(currentScene);
        }
    }

}
