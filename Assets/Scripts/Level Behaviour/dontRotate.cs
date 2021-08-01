using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontRotate : MonoBehaviour
{
    public GameObject player;
    public ParticleSystem particle;
    bool canIMove = true;

    private void Awake()
    {
        particle.Stop();
    }

    private void Start()
    {
        particle.Stop();
        EventManager.UnSubscribe("StopHeavyVFXMoving", Stop);
        EventManager.Subscribe("StopHeavyVFXMoving", Stop);
    }

    void Update()
    {
        if(canIMove)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.15f, player.transform.position.z);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        particle.Play();
    }

    public void Stop(object[] parameters)
    {
        StartCoroutine(StopMovement());
    }

    IEnumerator StopMovement()
    {
        canIMove = false;
        yield return new WaitForSeconds(3f);
        canIMove = true;
    }
}
