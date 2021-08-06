using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    public float hp;
    public float shield;

    public List<Transform> spawnPositions;
    public Animator animator;
    public GameObject player;
    public SphereCollider radar;
    public float attackTime;
    Transform _previousPosition;
    int _attackCount;

    public GameObject fakeAttackObject;
    public GameObject fireSpitPrefab;
    public GameObject airSpitPrefab;
    public GameObject airSpitDecoyPrefab;
    public Transform attackSpawnPoint;
    public Transform airAttackSpawnPoint;
    public int airAttackAmount;

    public ParticleSystem blood;
    public Image shieldBar;
    public Image hpBar;
    public GameObject wholeUI;

    float _originalHP;
    float _originalShield;
    bool _reset = false;
    bool _broke = false;
    bool _died = false;

    private void Start()
    {
        _originalHP = hp;
        _originalShield = shield;
        ChooseSpawnPoint();
        StartCoroutine(MoveSnakePosition());
    }

    private void Update()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if(shield <= 0 && !_broke)
        {
            _broke = true;
            attackTime = attackTime / 3f;
            airAttackAmount *= 2;
            SoundManager.instance.PlaySound(SoundID.SNAKE_SHIELDBREAK);
        }
        
        if(hp <= 0 && !_died)
        {
            _died = true;
            wholeUI.SetActive(false);
            Debug.Log("mori");
            animator.Play("Die");
        }
    }

    public void ChooseSpawnPoint()
    {
        int rand = Random.Range(0, spawnPositions.Count);
        while(spawnPositions[rand] == _previousPosition)
        {
            rand = Random.Range(0, spawnPositions.Count);
        }

        _previousPosition = spawnPositions[rand];
        transform.position = spawnPositions[rand].position;
    }

    public void ResetAttackPattern()
    {
        if (!_reset && !_died)
        {
            _reset = true;
            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackTime);
        StartCoroutine(ChooseNextAction());
        
    }

    IEnumerator MoveSnakePosition()
    {
        if (!_died)
        {
            animator.Play("Hide");
            yield return new WaitForSeconds(1.5f);
            ChooseSpawnPoint();
            animator.Play("Appear");
        }
    }
    
    IEnumerator ChooseNextAction()
    {
        yield return new WaitForSeconds(attackTime / 2);
        if (_attackCount <= 2)
        {
            GenerateAttack();
        }
        else {
            StartCoroutine(MoveSnakePosition());
            _attackCount = 0;
        }
        _reset = false;
    }

    void GenerateAttack()
    {
        if (!_died)
        {
            int rand;
            if (Vector3.Distance(player.transform.position, transform.position) <= 6.5f)
            {
                rand = Random.Range(0, 4);
            }
            else rand = Random.Range(0, 2);

            switch (rand)
            {
                case 0:
                    LaserChargeUpAttack();
                    break;
                case 1:
                    UpwardsAttack();
                    break;
                case 2:
                    CloseMeleeAttack(1);
                    break;
                case 3:
                    CloseMeleeAttack(2);
                    break;
            }

            _attackCount++;
        }
    }

    public void LaserChargeUpAttack()
    {
        StopCoroutine(Charge());
        StartCoroutine(Charge());
    }

    IEnumerator Charge()
    {
        animator.Play("Charge1");
        yield return new WaitForSeconds(0.75f);
        fakeAttackObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        fakeAttackObject.SetActive(false);
        GameObject bullet = Instantiate(fireSpitPrefab);
        bullet.transform.position = attackSpawnPoint.position;
        animator.Play("Charge2");
        yield return new WaitForSeconds(0.3f);
    }

    public void CloseMeleeAttack(int number)
    {
        switch (number)
        {
            case 1:
                animator.Play("Attack1");
                break;
            case 2:
                animator.Play("Attack2");
                break;
        }
    }

    public void UpwardsAttack()
    {
        animator.Play("UpwardShot");
        StartCoroutine(AirRaid());
    }



    IEnumerator AirRaid()
    {
        yield return new WaitForSeconds(1f);
        int raidAttacks = airAttackAmount;
        while(raidAttacks > 0)
        {
            raidAttacks--;
            CreateAirBullet();
            yield return new WaitForSeconds(0.15f);
        }
    }

    public void CreateAirBullet()
    {
        GameObject spit = Instantiate(airSpitPrefab);
        spit.transform.position = new Vector3(Random.Range(airAttackSpawnPoint.transform.position.x - 10, airAttackSpawnPoint.transform.position.x + 11),
                                                            airAttackSpawnPoint.transform.position.y,
                                                            Random.Range(airAttackSpawnPoint.transform.position.z - 10, airAttackSpawnPoint.transform.position.z + 11));
    }

    public void DecoySpit()
    {
        GameObject decoy = Instantiate(airSpitDecoyPrefab);
        decoy.transform.position = attackSpawnPoint.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "attackFX")
        {
            TakeDamage(1);
            EventManager.Trigger("AddSpecial", 0.005f);
        }

        if (other.gameObject.tag == "bigAttackFX")
        {
            TakeDamage(10f);
        }

        if (other.gameObject.tag == "heavyAttackFX")
        {
            TakeDamage(2f);
        }

        if (other.gameObject.tag == "specialAttackFX")
        {
            TakeDamage(0.66f);
        }
    }

    public void TakeDamage(float value)
    {
        if (_broke)
        {
            blood.Stop();
            blood.Play();
            SoundManager.instance.PlaySound(SoundID.BLOOD_1);
        }
        if (shield <= 0)
        {
            hp -= value;
            hpBar.fillAmount = hp / _originalHP;
        }
        else
        {
            shield -= value;
            shieldBar.fillAmount = shield / _originalShield;
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    public void PlaySFX(SoundID sound)
    {
        SoundManager.instance.PlaySound(sound);
    }

    public void EnableAttackCollider()
    {
        EventManager.Trigger("EnableSnakeMeleeColliders");
    }

    public void DisableAttackCollider()
    {
        EventManager.Trigger("DisableSnakeMeleeColliders");
    }
}
