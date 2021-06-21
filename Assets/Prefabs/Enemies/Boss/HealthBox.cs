using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    public Collider collider;
    public Boss boss;

    public void enableCollider()
    {
        collider.enabled = true;
    }

    public void disableCollider()
    {
        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "attackFX")
        {
            boss.hp--;
            SoundManager.instance.PlaySound(SoundID.ENEMY_DAMAGE);
        }

        if(other.gameObject.tag == "specialAttackFX")
        {
            boss.hp-=5;
            SoundManager.instance.PlaySound(SoundID.ENEMY_DAMAGE);
        }
    }
}
