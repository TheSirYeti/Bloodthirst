using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttacks : MonoBehaviour
{
    public bool chase;
    public GameObject target;
    public Transform parent;
    public float speed;
    public float chaseTime;

    private void Awake()
    {
        chase = false;
        transform.parent = parent;
        target = GameObject.FindWithTag("Player");
        SoundManager.instance.PlaySound(SoundID.BURN);
    }

    private void Update()
    {
        if (chase)
        {
            transform.parent = null;
            Vector3 targetYAdjust = new Vector3(target.transform.position.x, target.transform.position.y + 1f, target.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetYAdjust, speed * Time.deltaTime);
            StartCoroutine(selfDestruct());
        }
        else
        {
            transform.position = new Vector3(parent.position.x, parent.position.y + 0f, parent.position.z);
        }
    }

    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(chaseTime);
        SoundManager.instance.StopSound(SoundID.BURN);
        Destroy(gameObject);
    }

    public void enableChase()
    {
        chase = true;
    }

    public void setPosition()
    {
        transform.position = parent.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SoundManager.instance.StopSound(SoundID.BURN);
            //Destroy(gameObject);
        }
            
    }
}
