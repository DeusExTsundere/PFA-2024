using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class uiLife : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    private TextMeshProUGUI life;
    void Start()
    {
        life = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        life.SetText("Point de Vie : " + character.PointDeVie);
    }
}
