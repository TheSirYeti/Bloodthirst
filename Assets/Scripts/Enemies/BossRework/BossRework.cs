using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossRework : MonoBehaviour
{
    [Header("Stats")]
    public float hp;
    public float shield;
    public float walkingSpeed;
    public float minDistanceFromPlayer;

    [Header("Component References")]
    public Animator animator;

    [Header("Hiding Objects / Colliders")]
    public GameObject sword;
    public Collider swordCollider;
    public Collider attackCollider;
    public Transform resetPoint;


    [Header("Skeleton Properties")]
    public List<GameObject> skeletonSpawnpoints;
    public GameObject skeletonPrefab;

    [Header("VFX")]
    public List<ParticleSystem> vfxSlashEffects;
    public ParticleSystem blood;
    public GameObject laser;
    public GameObject laserAttack;

    [Header("Player References")]
    public GameObject player;

    [Header("UI References")]
    public Image hpBar;
    public Image shieldBar;
    public GameObject entireUI;

    bool moving = false;
    bool shieldBroke = false;
    bool died = false;
    float originalHP;
    float originalShield;

    //----------------------------------------------------------

    private void Start()
    {
        NextActionManager();
        originalHP = hp;
        originalShield = shield;
    }

    public void StartCooldown()
    {
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1.25f);
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        NextActionManager();
    }

    public void NextActionManager()
    {
        if (Vector3.Distance(transform.position, player.transform.position) >= minDistanceFromPlayer)
        {
            MoveBossPosition();
        }
        else
        {
            AttackPlayer();
        }
    }

    public void MoveBossPosition()
    {
        if (!died)
        {
            animator.Play("Walk");
            moving = true;
            StartCoroutine(WalkTowardsPlayer());
        }
    }

    IEnumerator WalkTowardsPlayer()
    {
        if (!died)
        {
            while (moving)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (walkingSpeed * Time.fixedDeltaTime));
                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
                yield return new WaitForSeconds(0.01f);
            }
            NextActionManager();
        }
    }

    public void AttackPlayer()
    {
        if (!died)
        {
            int rand = Random.Range(0, 7);
            switch (rand)
            {
                case 0:
                    animator.Play("BigAttack1");
                    break;
                case 1:
                    animator.Play("BigAttack2");
                    break;
                case 2:
                    animator.Play("BigAttack3");
                    break;
                case 3:
                    animator.Play("BigAttack4");
                    break;
                case 4:
                    animator.Play("BasicSlash1");
                    break;
                case 5:
                    animator.Play("Summon1");
                    break;
                case 6:
                    animator.Play("Kamehameha");
                    break;
            }
        }
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) <= minDistanceFromPlayer)
        {
            moving = false;
        }

        if(shield <= 0 && !shieldBroke)
        {
            shieldBroke = true;
            SoundManager.instance.PlaySound(SoundID.SHIELD_BREAK);
        }

        if(hp <= 0 && !died)
        {
            died = true;
            animator.Play("Die");
            entireUI.SetActive(false);
            swordCollider.enabled = false;
            attackCollider.enabled = false;
            EventManager.Trigger("EndLongFade");
        }

        if(transform.position.y <= -200)
        {
            transform.position = resetPoint.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "attackFX")
        {
            TakeDamage(1);
            EventManager.Trigger("AddSpecial", 0.01f);
        }

        if (other.gameObject.tag == "heavyAttackFX")
        {
            TakeDamage(1.5f);
            EventManager.Trigger("AddSpecial", 0.01f);
        }

        if (other.gameObject.tag == "specialAttackFX")
        {
            TakeDamage(1);
        }

        if (other.gameObject.tag == "bigAttackFX")
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(float amount)
    {
        if (!shieldBroke)
        {
            SoundManager.instance.PlaySound(SoundID.BOSS_HIT);
            shield -= amount;
            shieldBar.fillAmount = shield / originalShield;
        }
        else
        {
            blood.Stop();
            blood.Play();
            SoundManager.instance.PlaySound(SoundID.BLOOD_1);
            hp -= amount;
            hpBar.fillAmount = hp / originalHP;
        }
    }

    public void SummonSkeleton()
    {
        SoundManager.instance.PlaySound(SoundID.BONES);
        GameObject skeleton1 = Instantiate(skeletonPrefab);
        skeleton1.transform.position = skeletonSpawnpoints[0].transform.position;
        GameObject skeleton2 = Instantiate(skeletonPrefab);
        skeleton2.transform.position = skeletonSpawnpoints[1].transform.position;
        GameObject skeleton3 = Instantiate(skeletonPrefab);
        skeleton3.transform.position = skeletonSpawnpoints[2].transform.position;
        GameObject skeleton4 = Instantiate(skeletonPrefab);
        skeleton4.transform.position = skeletonSpawnpoints[3].transform.position;
    }

    public void EnableLaser()
    {
        laser.SetActive(true);
        SoundManager.instance.PlaySound(SoundID.LIGHTNING);
    }

    public void DisableLaser()
    {
        laser.SetActive(false);
    }

    public void EnableAttackLaser()
    {
        laserAttack.SetActive(true);
    }

    public void DisableAttackLaser()
    {
        laserAttack.SetActive(false);
    }

    public void EnableSwordCollider()
    {
        swordCollider.enabled = true;
        attackCollider.enabled = true;
    }

    public void DisableSwordCollider()
    {
        swordCollider.enabled = false;
        attackCollider.enabled = false;
    }

    public void PlaySlashVFX(int index)
    {
        vfxSlashEffects[index].Stop();
        vfxSlashEffects[index].Play();
    }

    public void PlaySFX(SoundID sound)
    {
        SoundManager.instance.PlaySound(sound);
    }
}
