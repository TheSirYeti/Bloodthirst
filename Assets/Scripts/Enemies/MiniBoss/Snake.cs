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

    private void Start()
    {
        ChooseSpawnPoint();

        StartCoroutine(MoveSnakePosition());
    }

    private void Update()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
    }

    public void ChooseSpawnPoint()
    {
        int rand = Random.Range(0, spawnPositions.Count);
        if(spawnPositions[rand] != _previousPosition)
        {
            _previousPosition = spawnPositions[rand];
            transform.position = spawnPositions[rand].position;
        }
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
        yield return new WaitForSeconds(3f);
        if (_attackCount <= 2)
        {
            GenerateAttack();
        }
        else StartCoroutine(MoveSnakePosition());
    }

    void GenerateAttack()
    {
        int rand;
        if (Vector3.Distance(player.transform.position, transform.position) <= 4)
        {

        }
    }

    public void LaserChargeUpAttack()
    {

    }

    public void CloseMeleeAttack()
    {

    }

    public void UpwardsAttack()
    {

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
