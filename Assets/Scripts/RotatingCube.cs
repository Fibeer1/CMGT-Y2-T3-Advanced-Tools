using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotatingCube : MonoBehaviour
{
    //Used to keep the spawned objects from staying in one place and not colliding
    [SerializeField] private Vector3 rotationDirection;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        transform.Rotate(rotationDirection * rotationSpeed);
    }
}
