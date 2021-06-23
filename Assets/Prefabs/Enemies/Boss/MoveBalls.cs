using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBalls : MonoBehaviour
{
    float timeToDie;
    private void Start()
    {
        timeToDie = Time.time + 15f;
    }

    private void Update()
    {
        if (timeToDie < Time.time)
            Destroy(gameObject);
    }
}
