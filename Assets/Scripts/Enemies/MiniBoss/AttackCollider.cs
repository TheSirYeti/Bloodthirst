using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public Collider collider;

    private void Start()
    {
        EventManager.UnSubscribe("EnableSnakeMeleeColliders", EnableCollider);
        EventManager.UnSubscribe("DisableSnakeMeleeColliders", DisableCollider);
        EventManager.Subscribe("EnableSnakeMeleeColliders", EnableCollider);
        EventManager.Subscribe("DisableSnakeMeleeColliders", DisableCollider);
    }

    public void EnableCollider(object[] parameters)
    {
        collider.enabled = true;
    }

    public void DisableCollider(object[] parameters)
    {
        collider.enabled = false;
    }
}
