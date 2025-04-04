using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotatingCube : MonoBehaviour
{
    [SerializeField] private Vector3 rotationDirection;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        transform.Rotate(rotationDirection * rotationSpeed);
    }
}
