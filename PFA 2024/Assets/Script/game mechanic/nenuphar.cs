using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nenuphar : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private const string ANIMATOR_EVENT = "Descente";

    private void OnTriggerEnter(Collider other)
    {
        StateTrigger();
    }

    public void StateTrigger()
    {
        animator?.SetTrigger(ANIMATOR_EVENT);
    }
}
