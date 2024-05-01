using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseUi;
    [SerializeField] private Animator _animator;
    private bool _paused = false;
    private float inputTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _paused == false)
        {
            _paused = true;
            StartCoroutine(pause());   
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && _paused == true) 
        {
            _paused = false;
            pauseUi.SetActive(true);
            //StartCoroutine(unPause());
            _animator.Play("PauseAnimation");
        }
    }

    IEnumerator pause()
    {
        _animator.Play("unPauseAnimation");
        yield return new WaitForSeconds(1);
        pauseUi.SetActive(false);
    }

    IEnumerator unPause()
    {
        yield return new WaitForSeconds(1);
    }
}
