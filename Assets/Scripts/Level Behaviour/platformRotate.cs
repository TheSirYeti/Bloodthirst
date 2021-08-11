using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformRotate : MonoBehaviour
{ 
    void FixedUpdate()
    {
        Vector3 position = new Vector3();
        if (GetComponent<Renderer>() != null)
            position = GetComponent<Renderer>().bounds.center;
        if(position != null)
            transform.RotateAround(position, new Vector3(0f,18f,0f), 15f * Time.deltaTime);
    }
}
