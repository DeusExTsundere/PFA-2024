using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reset : MonoBehaviour
{
    private string currentScene;
    private void Start()
    {
        currentScene =  SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F2))
        {
            SceneManager.LoadScene(currentScene);
        }
    }
}
