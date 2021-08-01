using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingRotation : MonoBehaviour
{
    public Vector3 rotationValues;
    void Update()
    {
        transform.Rotate(rotationValues);
    }
}
