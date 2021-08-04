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
    Transform _previousPosition;
    int _attackCount;

    public GameObject fakeAttackObject;
    public GameObject fireSpitPrefab;
    public GameObject airSpitPrefab;
    public GameObject airSpitDecoyPrefab;
    public Transform attackSpawnPoint;
    public Transform airAttackSpawnPoint;

    public ParticleSystem blood;
    public Image shieldBar;
    public Image hpBar;


    bool _reset = false;
    bool _broke = false;
    bool _died = false;

    private void Start()
    {
        ChooseSpawnPoint();
        StartCoroutine(MoveSnakePosition());
    }

    private void Update()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if(hp <= 0 && !_died)
        {
            _died = true;
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
        yield return new WaitForSeconds(1f);
        Debug.Log(2);
        StartCoroutine(ChooseNextAction());
        
    }

    IEnumerator MoveSnakePosition()
    {
        animator.Play("Hide");
        yield return new WaitForSeconds(1.5f);
        ChooseSpawnPoint();
        animator.Play("Appear");
    }
    
    IEnumerator ChooseNextAction()
    {
        yield return new WaitForSeconds(1f);
        if (_attackCount <= 2)
        {
            Debug.Log(2.1f);
            GenerateAttack();
        }
        else {
            Debug.Log(2.2f);
            StartCoroutine(MoveSnakePosition());
            _attackCount = 0;
        }
        _reset = false;
    }

    void GenerateAttack()
    {
        int rand;
        if (Vector3.Distance(player.transform.position, transform.position) <= 4)
        {
            rand = Random.Range(0, 4);
        }
        else rand = Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                Debug.Log(3.1);
                LaserChargeUpAttack();
                break;
            case 1:
                Debug.Log(3.2);
                UpwardsAttack();
                break;
            case 2:
                Debug.Log(3.3);
                CloseMeleeAttack(1);
                break;
            case 3:
                Debug.Log(3.3);
                CloseMeleeAttack(2);
                break;
        }

        _attackCount++;
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
        int raidAttacks = 18;
        while(raidAttacks > 0)
        {
            raidAttacks--;
            CreateAirBullet();
            yield return new WaitForSeconds(0.3f);
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
        }

        if (other.gameObject.tag == "bigAttackFX")
        {
            TakeDamage(10f);
        }

        if (other.gameObject.tag == "specialAttackFX")
        {
            TakeDamage(0.66f);
        }
    }

    public void TakeDamage(float value)
    {
        blood.Stop();
        blood.Play();
        if (shield <= 0)
            hp -= value;
        else shield -= value;
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
