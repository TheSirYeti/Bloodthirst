using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTest : MonoBehaviour
{
    [SerializeField]
    protected int damage;

    virtual protected void OnTriggerEnter(Collider other)
    {
        EnemyBase hitEntity = other.gameObject.GetComponent<EnemyBase>();
        if (hitEntity != null)
        {
            hitEntity.ReceiveDamage(damage);
        }
    }
}
