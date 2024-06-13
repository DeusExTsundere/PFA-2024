using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    private TextMeshProUGUI chrono;
    [SerializeField] private float temps;
    private string affichage;

    void Start()
    {
        chrono = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.FloorToInt(temps) > 0)
        {
            temps -= Time.deltaTime;
        }
        int secondes = Mathf.FloorToInt(temps);
        chrono.SetText("Timer : " + secondes );
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("levelChrono", Mathf.FloorToInt(temps));
    }
}
