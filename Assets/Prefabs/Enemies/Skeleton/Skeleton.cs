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
    float _attackCooldown = 2f;
    public GameObject deathExplosion;

    private void Start()
    {
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
                    break;
                case false:
                    animator.SetTrigger("attack");
                    transform.LookAt(null);
                    yield return new WaitForSeconds(_attackCooldown / 2);
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
        hp--;
    }

    public IEnumerator Die()
    {
        GameObject effect = Instantiate(deathExplosion);
        effect.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }

    public void chargeSound()
    {
        SoundManager.instance.PlaySound(SoundID.CHARGELASER);
    }

    public void releaseSound()
    {
        SoundManager.instance.PlaySound(SoundID.RELEASE_FIRE);
    }
}
