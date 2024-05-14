using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    [SerializeField] private bool crossable = false;
    public bool getTrough { get {  return crossable; } }
}
