using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    public Collider collider;
    public Boss boss;

    private void Update()
    {
        if (boss.hp <= 0)
        {
            boss.amDying();
        }
    }

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
            EventManager.Trigger("AddSpecial", 0.005f);
            EventManager.Trigger("HurtBoss");
            SoundManager.instance.PlaySound(SoundID.ENEMY_DAMAGE);
        }

        if(other.gameObject.tag == "specialAttackFX")
        {
            boss.hp-=5;
            EventManager.Trigger("HurtBoss");
            SoundManager.instance.PlaySound(SoundID.ENEMY_DAMAGE);
        }
    }
}
