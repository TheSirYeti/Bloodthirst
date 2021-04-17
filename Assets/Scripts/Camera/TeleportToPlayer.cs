using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToPlayer : MonoBehaviour
{

    public Transform target;
    public float smoothingTime = 0.005f;
    
    void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, target.position, smoothingTime);
    }
}
