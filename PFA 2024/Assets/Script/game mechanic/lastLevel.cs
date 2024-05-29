using UnityEngine;
using TMPro;
using System.Collections;

public class lastLevel : MonoBehaviour
{
    [SerializeField] private GameObject actualScoring;
    private float top;
    private TextMeshProUGUI highscore;

    private void Awake()
    {
        highscore = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        if (PlayerPrefs.GetFloat("score") > top)
        {
            StartCoroutine(newHighscore());
        }
        else
        {
            highscore.SetText("Actual Highscore :" + PlayerPrefs.GetFloat("score"));
            actualScoring.SetActive(true);
        }
    }

    IEnumerator newHighscore() 
    {
        highscore.SetText("Actual Higscore :" + top);
        yield return new WaitForSeconds(10);
        highscore.SetText("New Higscore :" + PlayerPrefs.GetFloat("score"));
        top = PlayerPrefs.GetFloat("score");
    }


}
