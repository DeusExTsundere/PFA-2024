using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCharacter : MonoBehaviour
{ 
    private InputPlayer player;

    private void Awake()
    {
        player = new InputPlayer();
    }

    private void OnEnable()
    {
        player.Player.Move.Enable();
    }

    private void OnDisable()
    {
        player.Player.Move.Disable();
    }
}
