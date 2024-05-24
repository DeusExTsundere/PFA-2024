using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    private TextMeshProUGUI chrono;
    [SerializeField] private float temps;

    void Start()
    {
        chrono = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (temps > 0)
        {
            temps -= Time.deltaTime;
        }
        chrono.SetText("Timer : " + temps);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("levelChrono", temps);
    }
}
