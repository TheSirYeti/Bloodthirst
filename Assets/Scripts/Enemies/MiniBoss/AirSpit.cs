using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSpit : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        StartCoroutine(Die());
        SoundManager.instance.PlaySound(SoundID.ACID_BURN);
    }

    void FixedUpdate()
    {
        transform.position += Vector3.down * speed * Time.fixedDeltaTime;
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(7);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Floor" || other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
