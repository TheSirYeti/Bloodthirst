using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float hp;
    public HealthBox healthCollider;
    public DamageBox damageCollider;
    public GameObject player;

    public Animator animator;
    float _attackCooldown;
    public float cooldown;

    public int attackCounter;
    public bool attacking;

    public GameObject ringPrefab;
    public GameObject bossKamikaze;

    bool _point = false;
    private void Awake()
    {
        StartCoroutine(rest());
        animator.SetBool("notAttacking", false);
    }

    private void FixedUpdate()
    {
        attack();
    }

    void attack()
    {
        if (!animator.GetBool("resting"))
        {
            move();
            if(_attackCooldown <= Time.time)
            {
                if (animator.GetBool("inRange"))
                {
                    Debug.Log("NO?");
                    animator.SetBool("inRange", true);
                    animator.SetBool("notAttacking", false);
                    int rand;
                    rand = Random.Range(0, 2);
                    switch (rand)
                    {
                        case 0:
                            animator.SetTrigger("attack");
                            _attackCooldown = cooldown + Time.time;
                            break;
                        case 1:
                            animator.SetTrigger("sweep");
                            _attackCooldown = cooldown + Time.time;
                            break;
                    }
                    playRandomVoiceAttack();
                    attackCounter++;
                }
                else
                {
                    animator.SetBool("notAttacking", false);
                    animator.SetTrigger("point");
                    _attackCooldown = cooldown + Time.time;
                }
            }

            if (attackCounter >= 4)
            {
                attackCounter = 0;
                StartCoroutine(rest());
            }
        }
    }

    IEnumerator rest()
    {
        animator.SetBool("resting", true);
        healthCollider.enableCollider();
        yield return new WaitForSeconds(10f);
        healthCollider.disableCollider();
        animator.SetBool("resting", false);
    }

    void move() {
        if (Time.timeScale != 0f && !checkAttackDistance())
        {
            if (!animator.GetBool("point"))
            {
                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
                Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
                transform.position = Vector3.Lerp(transform.position, playerPos, 0.005f); //Vector3.MoveTowards(transform.position, playerPos, 3.5f);
                animator.SetBool("inRange", false);
            } else animator.SetBool("notAttacking", false);
        }
        else { animator.SetBool("inRange", true); animator.SetBool("notAttacking", false);  }
    }

    bool checkAttackDistance()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 7f)
        {
            return true;
        }
        else return false;
    }

    public void summonRing()
    {
        GameObject ring = Instantiate(ringPrefab);
        ring.transform.position = new Vector3(transform.position.x, ring.transform.position.y, transform.position.z);
    }

    public void summonKamikaze()
    {
        playRandomVoiceAttack();
        GameObject kamikaze = Instantiate(bossKamikaze);
        kamikaze.transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
    }

    public void isAttacking()
    {
        attacking = true;
        animator.SetBool("notAttacking", false);
    }

    public void notAttacking()
    {
        attacking = false;
        animator.SetBool("notAttacking", true);
    }

    public void enableDamageCollider()
    {
        damageCollider.enableCollider();
    }

    public void disableDamageCollider()
    {
        damageCollider.disableCollider();
    }

    public void pointing()
    {
        _point = !_point;
    }

    public void playRandomVoiceAttack()
    {
        int rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                SoundManager.instance.PlaySound(SoundID.BOSS_VOICE1);
                break;
            case 1:
                SoundManager.instance.PlaySound(SoundID.BOSS_VOICE2);
                break;
            case 2:
                SoundManager.instance.PlaySound(SoundID.BOSS_VOICE3);
                break;
        }
    }
}
