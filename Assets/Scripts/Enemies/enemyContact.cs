using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyContact : MonoBehaviour
{
    public Rigidbody rigidBody;
    private Vector3 pushPosition;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "attackFX")
        {
            pushPosition = rigidBody.transform.position - other.transform.position;
            rigidBody.AddForce(pushPosition.normalized * 1000f);
        }
    }
}
