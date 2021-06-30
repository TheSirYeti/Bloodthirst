using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float timeToDie = 5f;
    float timer;
    private void Start()
    {
        timer = timeToDie + Time.time;
    }
    private void Update()
    {
        if(timer < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
