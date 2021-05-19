using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public GameObject enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "attackFX")
        {
            enemy.GetComponent<EnemyBase>().ReceiveDamage(1);
        }
    }
}
