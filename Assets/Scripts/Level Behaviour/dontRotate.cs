using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontRotate : MonoBehaviour
{
    public GameObject player;
    public ParticleSystem particle;

    private void Awake()
    {
        particle.Stop();
    }

    private void Start()
    {
        particle.Stop();
    }

    void Update()
    {
        transform.position = player.transform.position;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        particle.Play();
    }
}
