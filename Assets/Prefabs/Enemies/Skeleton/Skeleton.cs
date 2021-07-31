using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public GameObject player;
    public GameObject attack;
    public Animator animator;
    public Transform attackSpawnPoint;
    public float maximumAttackDistance;
    float _attackCooldown = 5f;
    public GameObject deathExplosion;
    public GameObject shield;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(makeAttack());
        transform.LookAt(player.transform.position);
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= maximumAttackDistance)
        {
            if (Vector3.Distance(transform.position, player.transform.position) >= 4f)
            {
                animator.SetBool("playerInRange", true);
            }
            else
            {
                animator.SetBool("playerInRange", false);
            }
        }

        if(hp <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator makeAttack()
    {
        while (true)
        {
            switch (animator.GetBool("playerInRange"))
            {
                case true:
                    if (Vector3.Distance(transform.position, player.transform.position) <= maximumAttackDistance)
                    {
                        animator.SetTrigger("attack");
                    }
                    yield return new WaitForSeconds(_attackCooldown);
                    transform.LookAt(player.transform.position);
                    break;
                case false:
                    if (Vector3.Distance(transform.position, player.transform.position) <= maximumAttackDistance)
                    {
                        animator.SetTrigger("attack");
                    }
                    yield return new WaitForSeconds(_attackCooldown);
                    transform.LookAt(player.transform.position);
                    break;
            }
        }
    }

    public void createBullet()
    {
        GameObject bullet = Instantiate(attack);
        bullet.GetComponent<FireAttacks>().parent = attackSpawnPoint;
        bullet.GetComponent<FireAttacks>().setPosition();
        bullet.GetComponent<FireAttacks>().chase = true;
    }

    public override void takeDamage()
    {
        EventManager.Trigger("AddSpecial", 0.005f);
        Debug.Log("EN UNA VILLA NACIO FUE DESEO DE DIOS CRECER Y SOBREVIVIR");
        SoundManager.instance.PlaySound(SoundID.BLOOD_1);
        blood.Stop();
        blood.Play();
        hp--;
    }

    public IEnumerator Die()
    {
        blood.Stop();
        blood.Play();
        animator.SetTrigger("died");
        SoundManager.instance.StopSound(SoundID.BURN);
        blood.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    void enableShield()
    {
        vunerable = false;
        shield.GetComponent<SkeletonShield>().resetValues();
    }

    void checkShield()
    {
        if (!shield.activeSelf)
        {
            vunerable = true;
        }
    }

    public void chargeSound()
    {
        SoundManager.instance.PlaySound(SoundID.CHARGELASER);
    }

    public void releaseSound()
    {
        SoundManager.instance.PlaySound(SoundID.RELEASE_FIRE);
    }

    public void deathSound()
    {
        SoundManager.instance.PlaySound(SoundID.ENEMY_DEATH);
    }
}
