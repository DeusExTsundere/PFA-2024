using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class uiLife : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    private TextMeshProUGUI life;

    private void Awake()
    {
        life = GetComponent<TextMeshProUGUI>();
        life.SetText("Point de Vie : " + character.PointDeVie);        
    }



    // Update is called once per frame
    void Update()
    {
        life.SetText("Point de Vie : " + character.PointDeVie);
    }
}
