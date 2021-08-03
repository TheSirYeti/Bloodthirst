using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float hp;

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

    bool _reset = false;
    bool _died = false;

    private void Start()
    {
        ChooseSpawnPoint();

        StartCoroutine(MoveSnakePosition());
    }

    private void Update()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if(hp <= 0)
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
        Debug.Log("RESET");
        if (!_reset)
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
            rand = Random.Range(0, 3);
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
                CloseMeleeAttack();
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

    public void CloseMeleeAttack()
    {
        int rand = Random.Range(0, 2);

        switch (rand)
        {
            case 0:
                animator.Play("Attack1");
                break;
            case 1:
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

        }

        if (other.gameObject.tag == "bigAttackFX")
        {

        }

        if (other.gameObject.tag == "specialAttackFX")
        {

        }
    }

}
