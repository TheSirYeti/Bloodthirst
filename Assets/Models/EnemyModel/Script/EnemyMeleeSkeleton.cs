using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using Player.Behaviour;

public class EnemyMeleeSkeleton : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField]
    protected int enemyLife = 100;
    [SerializeField]
    protected int currentHitLife;
    [SerializeField]
    protected float timeDestroy;
    [SerializeField]
    protected CapsuleCollider enemyBox;
    protected Action changeEst;
    [SerializeField]
    protected ParticleSystem blood;


    [SerializeField]
    protected CapsuleCollider DamageCollider;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Movement _player;
    [SerializeField]
    private Rigidbody RbEnemy;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float lineOfSite;
    [SerializeField]
    private float aboutPlayer;
    private float distanceFromPlayer;
    protected int RandomAttack;
    [SerializeField]
    private float TimeAttack;

    [Header("Target")]
    [SerializeField]
    protected Transform player;
    protected Vector3 targetPlayerPosition;

    void Start()
    {
        changeEst = MovementGeneric;
    }

    // Update is called once per frame
    void Update()
    {
        changeEst();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "attackFX")
        {
            EventManager.Trigger("AddSpecial", 0.005f);
            ReceiveDamage(1);
            lineOfSite = 9999;
            SoundManager.instance.PlaySound(SoundID.BLOOD_1);
            blood.Stop();
            blood.Play();
        }

        if (other.gameObject.tag == "heavyAttackFX")
        {
            EventManager.Trigger("AddSpecial", 0.01f);
            ReceiveDamage(2);
            lineOfSite = 9999;
            SoundManager.instance.PlaySound(SoundID.BLOOD_1);
            blood.Stop();
            blood.Play();
        }

        if (other.gameObject.tag == "bigAttackFX")
        {
            EventManager.Trigger("AddSpecial", 0.005f);
            ReceiveDamage(11);
            lineOfSite = 9999;
            SoundManager.instance.PlaySound(SoundID.BLOOD_1);
            blood.Stop();
            blood.Play();
        }

        if (other.gameObject.tag == "specialAttackFX")
        {
            EventManager.Trigger("AddSpecial", 0.005f);
            ReceiveDamage(1);
            lineOfSite = 9999;
            SoundManager.instance.PlaySound(SoundID.BLOOD_1);
            blood.Stop();
            blood.Play();
        }
    }

    public virtual void ReceiveDamage(int amoutDamage)
    {
        if (amoutDamage > 0 && currentHitLife > 0)
        {
            currentHitLife -= amoutDamage;

            if (currentHitLife <= 0)
            {
                aboutPlayer = 0;
                lineOfSite = 0;
                animator.SetTrigger("Die");
                blood.gameObject.SetActive(false);
                GetComponent<EnemyMeleeSkeleton>().enabled = false;
                
            }
            else if (currentHitLife > 0)
            {
                aboutPlayer = 0;
                lineOfSite = 0;

                animator.SetTrigger("Receive");
                changeEst = ActiveGizmos;
            }
        }
    }


    private void MovementGeneric()
    {
        targetPlayerPosition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
        distanceFromPlayer = Vector3.Distance(player.position, transform.position);

        if(distanceFromPlayer < lineOfSite && distanceFromPlayer > aboutPlayer)
        {
            animator.SetFloat("Walking", 1);
            transform.position = Vector3.MoveTowards(this.transform.position, player.position, (speed * Time.deltaTime));
            transform.LookAt(targetPlayerPosition);
        }
        else if(distanceFromPlayer <= aboutPlayer)
        {
            aboutPlayer = 0;
            lineOfSite = 0;
            animator.SetFloat("Walking", 0);
            animator.SetTrigger("AttackMelee");

        }
        else
        {
            animator.SetFloat("Walking", 0);
        }
    }

    private void ActiveGizmos()
    {
        StartCoroutine(ActiveMovement());

        IEnumerator ActiveMovement()
        {
            yield return new WaitForSeconds(TimeAttack);
            aboutPlayer = 1.5F;
            lineOfSite = 9999;
            changeEst = MovementGeneric;
        }
    }
    private void EnemyAttackActive()
    {
        DamageCollider.enabled = true;

    }
    private void EnemyAttackDesactive()
    {
        DamageCollider.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, aboutPlayer);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
