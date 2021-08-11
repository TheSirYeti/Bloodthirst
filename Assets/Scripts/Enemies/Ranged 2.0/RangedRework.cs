using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedRework : Enemy
{
    [Header("Main Arguments")]
    public GameObject fireAttackPrefab;
    public float minimumAttackDistance;
    [SerializeField] float currentDistance;
    public float minimumMeleeDistance;
    public Transform attackSpawnPoint;
    public float attackCooldown;
    public Animator animator;
    public GameObject player;
    public List<ParticleSystem> slashes;

    [Header("Colliders")]
    public Collider leftHandCollider;
    public Collider rightHandCollider;
    public Collider attackCollider;

    [SerializeField]bool isInRange = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        currentDistance = Vector3.Distance(transform.position, player.transform.position);
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if(currentDistance <= minimumAttackDistance && !isInRange)
        {
            isInRange = true;
            StartCooldown();
        }

        if (currentDistance >= minimumAttackDistance && isInRange)
        {
            isInRange = false;
        }

        if(hp <= 0)
        {
            animator.Play("death");
        }
    }

    public override void takeDamage(int amount)
    {
        minimumAttackDistance = 40f;
        EventManager.Trigger("AddSpecial", 0.005f);
        SoundManager.instance.PlaySound(SoundID.BLOOD_1);
        blood.Stop();
        blood.Play();
        hp -= amount;
    }

    public void PlaySFX(SoundID sound)
    {
        SoundManager.instance.PlaySound(sound);
    }

    public void createBullet()
    {
        GameObject bullet = Instantiate(fireAttackPrefab);
        bullet.GetComponent<FireAttacks>().parent = attackSpawnPoint;
        bullet.GetComponent<FireAttacks>().setPosition();
        bullet.GetComponent<FireAttacks>().chase = true;
    }

    public void StartCooldown()
    {
        if(isInRange)
            StartCoroutine(SetUpAttack());
    }

    IEnumerator SetUpAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        
        if(Vector3.Distance(transform.position, player.transform.position) >= minimumMeleeDistance)
        {
            animator.Play("castAttack");
        }
        else
        {
            animator.Play("TwoHander");
        }
    }

    public void EnableAttackColliders()
    {
        leftHandCollider.enabled = true;
        rightHandCollider.enabled = true;
        attackCollider.enabled = true;
    }

    public void DisableAttackColliders()
    {
        leftHandCollider.enabled = false;
        rightHandCollider.enabled = false;
        attackCollider.enabled = false;
    }

    public void EnableVFX()
    {
        slashes[0].Play();
        slashes[1].Play();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
