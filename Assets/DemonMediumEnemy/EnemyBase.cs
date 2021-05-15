using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Base life")]
    [SerializeField]
    protected int life = 100;
    protected int currentHitLife;
    [SerializeField]
    private float TimeDestroy;

    [Header("Generic Movement")]
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float aboutPlayer;
    [SerializeField]
    protected float lineOfSite;
    [SerializeField]
    protected GameObject CapsuleDamage;
    protected float distanceFromPlayer;
    [SerializeField]
    protected Animator animator;
    [SerializeField]
    protected int RandomAttack;
    [SerializeField]
    protected int rotationSpeed;

    [Header("Target")]
    [SerializeField]
    protected Transform player;
    protected Vector3 targetPlayerPosition;
    protected bool activeDie;

    public abstract void MovementGeneric();
    virtual protected void Awake()
    {
        currentHitLife = life;
    }

    public virtual void ReceiveDamage(int amount)
    {
        if (amount > 0 && currentHitLife > 0)
        {
            currentHitLife -= amount;

            if (currentHitLife <= 0)
            {
                activeDie = true;

                this.gameObject.GetComponent<EnemyBase>().enabled = false;

                animator.SetTrigger("DieEnemy");
                this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                this.gameObject.GetComponent<Rigidbody>().useGravity = false;

                Destroy(this.gameObject, TimeDestroy);

            }
            else if (animator != null)
            {
                animator.SetTrigger("ReceiveDamage");
            }

        }
    }


    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, aboutPlayer);
    }

    protected IEnumerator Waiting(float time, Action accion)
    {
        yield return new WaitForSeconds(time);
        accion();
    }
}
