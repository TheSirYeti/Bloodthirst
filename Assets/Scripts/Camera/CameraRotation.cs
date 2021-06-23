using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        transform.rotation = Quaternion.Euler(-43f, 0, 0);
    }

}
