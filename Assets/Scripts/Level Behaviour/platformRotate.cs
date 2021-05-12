using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformRotate : MonoBehaviour
{ 
    void FixedUpdate()
    {
        Vector3 position = GetComponent<Renderer>().bounds.center;

        transform.RotateAround(position, new Vector3(0f,3f,0f), 15f * Time.deltaTime);
    }
}
