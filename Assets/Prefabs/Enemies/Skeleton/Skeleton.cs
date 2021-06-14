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
    float _attackCooldown = 9f;
    public GameObject deathExplosion;
    public GameObject shield;
    public SpecialAttackBar bar;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(makeAttack());
        bar = GameObject.FindWithTag("SpecialAttackBar").GetComponent<SpecialAttackBar>();
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
        if (!shield.activeSelf)
        {
            bar.addValue(0.05f);
            hp--;
            if(hp > 0)
                StartCoroutine(enableShield());
        } 
    }

    public IEnumerator Die()
    {
        //GameObject effect = Instantiate(deathExplosion);
        //effect.SetActive(true);
        animator.SetTrigger("died");
        SoundManager.instance.StopSound(SoundID.BURN);
        //effect.transform.parent = transform;
        //effect.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    IEnumerator enableShield()
    {
        shield.SetActive(true);
        vunerable = false;
        yield return new WaitForSeconds(2f);
        vunerable = true;
        shield.SetActive(false);
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
