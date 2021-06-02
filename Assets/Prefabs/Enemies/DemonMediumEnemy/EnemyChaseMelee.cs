using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseMelee : EnemyBase
{
    void Update()
    {
        MovementGeneric();
    }

    public override void MovementGeneric()
    {
        distanceFromPlayer = Vector3.Distance(player.position, transform.position);
        targetPlayerPosition = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);

        if(distanceFromPlayer < lineOfSite && distanceFromPlayer > aboutPlayer)
        {
            animator.SetFloat("WalkingEnemy", 1);

            transform.position = Vector3.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);

            transform.LookAt(targetPlayerPosition);
            transform.position += transform.forward * Time.deltaTime;
        }
        else if(distanceFromPlayer <= aboutPlayer)
        {
            RandomAttack = Random.Range(0, 10);

            if(RandomAttack >= 5)
            {
                animator.SetTrigger("AttackEnemy");
                transform.LookAt(this.transform.position);

                speed = 0;
                aboutPlayer = 0;
                lineOfSite = 0;

                if (!activeDie)
                {
                    StartCoroutine(Waiting(1.5f, Aux));
                }
            }
            else if(RandomAttack < 5)
            {
                animator.SetTrigger("AttackEnemy2");
                transform.LookAt(this.transform.position);

                speed = 0;
                aboutPlayer = 0;
                lineOfSite = 0;

                if (!activeDie)
                {
                    StartCoroutine(Waiting(1.5f, Aux));
                }
            }
        }
        else
        {
            animator.SetFloat("WalkingEnemy", 0);
        }
    }

    protected void Aux()
    {
        if (activeDie == false)
        {
            speed = 5;
            aboutPlayer = 1.2f;
            lineOfSite = 10;
        }
        else
        {
            speed = 0;
            aboutPlayer = 0;
            lineOfSite = 0;
        }

    }

    void PrefabsAttackActive()
    {
        CapsuleDamage.GetComponent<CapsuleCollider>().enabled = true;
    }
    void PrefabsAttackDesactive()
    {
        CapsuleDamage.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void playRandomGroan()
    {
        int chance = Random.Range(0, 2);
        if(chance == 1 && distanceFromPlayer < 25f)
        {
            int rand = Random.Range(1, 4);
            switch (rand)
            {
                case 1:
                    SoundManager.instance.PlaySound(SoundID.GROAN1);
                    break;
                case 2:
                    SoundManager.instance.PlaySound(SoundID.GROAN2);
                    break;
                case 3:
                    SoundManager.instance.PlaySound(SoundID.GROAN3);
                    break;
            }
        }
    }

    public void deathSound()
    {
        SoundManager.instance.PlaySound(SoundID.ENEMY_DEATH);
    }
}
