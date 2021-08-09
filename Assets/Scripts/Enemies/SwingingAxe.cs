using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingAxe : MonoBehaviour
{
    public Vector3 rotationValues;
    public Transform pivot;
    void FixedUpdate()
    {
        transform.Rotate(rotationValues);
    }
}
