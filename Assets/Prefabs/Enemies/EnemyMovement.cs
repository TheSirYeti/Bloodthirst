using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Enemy
{
    public  float speedRotationLerp;
    public  float lineOfSite;
    public  float aboutPlayer;
    private float wanderRadius = 3f;
    private float distancefromplayer;

    Vector3 targetPosition;
    Vector3 towardsTarget;


    [SerializeField] private Transform Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        RecalculeteTargetPosition();
    }
    void Update()
    {
        distancefromplayer = Vector3.Distance(Player.position, transform.position);

        if (distancefromplayer > lineOfSite && distancefromplayer > aboutPlayer)
        {
            Patrolling();
            speed = 1.3f;
        }
        else
        {
            MovementEnemyAttack();
            RecalculeteTargetPosition();
            speed = 5;
        }

        if(hp <= 0)
        {
            StartCoroutine(die());
        }
    }
    ///Funciones///
    //Recacula la posicion del target
    void RecalculeteTargetPosition()
    {
        targetPosition = transform.position + Random.insideUnitSphere * wanderRadius; //Una esfera con un radio aletorio.
        targetPosition.y = this.transform.position.y;
    }

    //Patrol//
    void Patrolling()
    {
        GetComponent<Renderer>().material.color = Color.green;

        towardsTarget = targetPosition - transform.position; //Se actuliza el Vector towardsTarget = restando la posicion destino por la posicion actual.
        if (towardsTarget.magnitude < 0.25f)                 
        {
            RecalculeteTargetPosition();
        }

        //Quaternion son matrices 4x4 que se utilizan para gestionar rotaciones, la explicacion matematica es mas compleja.
        Quaternion towardsTargetRotation = Quaternion.LookRotation(towardsTarget, Vector3.up);  //Nos permite especificar un vector hacia el cual queremos mirar y otro que corresponde con el eje de rotacion queremos usar.
        transform.rotation = Quaternion.Lerp(transform.rotation, towardsTargetRotation, speedRotationLerp * Time.deltaTime);  //Funcion de interpolacion propia de quaternion "Lerp" 
        transform.position += transform.forward * speed * Time.deltaTime;

        Debug.DrawLine(transform.position, targetPosition, Color.green);

        ///Es un rotacion instantanea///
        //transform.LookAt(targetPosition); //Se la pasa un punto en el espacio cual produce una rotacion tal que el vector forward mire hacia el punto de targetposition;
        //transform.position += transform.forward * speedEnemy * Time.deltaTime;
    }

    //Movimiento de ataque//
    void MovementEnemyAttack()
    {
        if (distancefromplayer < lineOfSite && distancefromplayer > aboutPlayer)
        {
            GetComponent<Renderer>().material.color = Color.yellow;

            transform.position = Vector3.MoveTowards(this.transform.position, Player.position, speed * Time.deltaTime);

            transform.LookAt(Player.position); //Se la pasa un punto en el espacio cual produce una rotacion tal que el vector forward mire hacia el punto de targetposition;
            transform.position += transform.forward * Time.deltaTime;
        }
        else if (distancefromplayer <= aboutPlayer)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public override void takeDamage(int amount)
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, aboutPlayer);
    }

}
