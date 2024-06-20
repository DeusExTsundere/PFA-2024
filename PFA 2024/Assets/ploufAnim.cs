using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ploufAnim : MonoBehaviour
{
    [SerializeField] private ParticleSystem ParticleSystem;
    private void OnTriggerEnter(Collider other)
    {
        ParticleSystem.Play();
    }
}
