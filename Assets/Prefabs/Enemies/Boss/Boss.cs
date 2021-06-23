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
    public GameObject kamikazePrefab;

    public Transform spawnPosition;

    bool amDead = false;

    bool _point = false;
    private void Awake()
    {
        StartCoroutine(rest());
        animator.SetBool("notAttacking", false);
        _point = false;
        healthCollider.enableCollider();
    }

    private void FixedUpdate()
    {
        attack();
    }

    void attack()
    {
        if (!animator.GetBool("resting") && !amDead)
        {
            if(animator.GetBool("notAttacking"))
                move();

            if(_attackCooldown <= Time.time)
            {
                if (animator.GetBool("inRange"))
                {
                    int rand;
                    rand = Random.Range(0, 3);
                    switch (rand)
                    {
                        case 0:
                            animator.Play("Point_summon");
                            _attackCooldown = cooldown + Time.time;
                            break;
                        case 1:
                            animator.Play("LegSweep");
                            _attackCooldown = cooldown + Time.time;
                            break;
                        case 2:
                            animator.Play("BasicAttack");
                            _attackCooldown = cooldown + Time.time;
                            break;

                    }
                    attackCounter++;
                }
                else
                {
                    int rand;
                    rand = Random.Range(0, 2);
                    switch (rand)
                    {
                        case 0:
                            animator.Play("Point_summon");
                            _attackCooldown = cooldown + Time.time;
                            break;
                        case 1:
                            animator.Play("LegSweep");
                            _attackCooldown = cooldown + Time.time;
                            break;
                    }
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
        animator.Play("Kneel");
        animator.SetBool("resting", true);
        //healthCollider.enableCollider();
        yield return new WaitForSeconds(10f);
        //healthCollider.disableCollider();
        animator.SetBool("resting", false);
    }

    void move() {
        if (Time.timeScale != 0f && !checkAttackDistance())
        {
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, playerPos, 0.01f); //Vector3.MoveTowards(transform.position, playerPos, 3.5f);
            animator.SetBool("inRange", false);
            animator.Play("Walk");
        }
        else { animator.SetBool("inRange", true); }
    }

    bool checkAttackDistance()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 1.3f)
        {
            return true;
        }
        else return false;
    }

    public void summonRing()
    {
        float angleStep = 360f / 15f;
        float angle = 0f;

        const float radius = 1f;

        for(int i = 0; i < 30; i++)
        {
            float ballDirectionX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float ballDirectionY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(ballDirectionX, ballDirectionY, 0f);
            Vector3 projectileMoveDirection = (projectileVector - transform.position).normalized * 20f;

            GameObject ball = Instantiate(ringPrefab, spawnPosition.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().velocity = new Vector3(projectileMoveDirection.x, 0, projectileMoveDirection.y);

            angle += angleStep;
        }
    }

    public void summonKamikaze()
    {
        StartCoroutine(RandomArea());
    }

    IEnumerator RandomArea()
    {
        bool flag = true;
        int counter = 4;

        while (flag)
        {
            float xPos = Random.Range(-7, 8);
            float zPos = Random.Range(-7, 8);

            GameObject kamikaze = Instantiate(kamikazePrefab);
            kamikaze.transform.position = new Vector3(spawnPosition.position.x + xPos, spawnPosition.position.y, spawnPosition.position.z + zPos);

            yield return new WaitForSeconds(1);

            counter--;

            if (counter <= 0)
            {
                flag = false;
            }
        }
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

    public void amDying()
    {
        amDead = true;
        animator.Play("Die");
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
