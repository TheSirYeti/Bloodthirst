using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpit : MonoBehaviour
{
    public Vector3 finalPosition;
    public float speed;

    private void Start()
    {
        finalPosition = GameObject.FindWithTag("Player").transform.position;
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, finalPosition) >= 0.2f)
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, speed);
        else Destroy(gameObject, 1.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor" || other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
