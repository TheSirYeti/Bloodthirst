using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float hp;
    public float speed;
    public Transform lookAtPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyLock")
        {
            other.gameObject.GetComponent<CameraLock>().addEnemy(gameObject);
        }

        if (other.gameObject.tag == "attackFX")
        {
            hp--;
            takeDamage();
        }
    }

    public abstract void takeDamage();
}
