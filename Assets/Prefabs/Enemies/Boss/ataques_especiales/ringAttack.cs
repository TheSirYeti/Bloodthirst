using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringAttack : MonoBehaviour
{
    float maxExpansionSize = 45f;

    private void FixedUpdate()
    {
        transform.localScale += new Vector3(0.2f, 0f, 0.2f);

        if(transform.localScale.x >= maxExpansionSize)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


}
