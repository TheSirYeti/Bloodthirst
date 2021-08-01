using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float bulletTime;
    public bool fire;
    public float speed;
    int _enemyCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        fire = false;
    }

    void Update()
    {
        transform.position -= transform.right * speed * Time.deltaTime;
        if (fire)
        {
            
            StartCoroutine(lifetime());
        }

        if(_enemyCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator lifetime()
    {
        yield return new WaitForSeconds(bulletTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            _enemyCount--;
        }
    }
}
