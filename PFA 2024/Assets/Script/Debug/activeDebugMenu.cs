using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeDebugMenu : MonoBehaviour
{
    [SerializeField] private GameObject debugMenu;
    private bool activ = false;

    private void Start()
    {
        debugMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            activ =!activ;
            debugMenu.SetActive(activ);
        }
    }
}
