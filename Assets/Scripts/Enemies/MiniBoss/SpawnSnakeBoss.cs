using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSnakeBoss : MonoBehaviour
{
    public GameObject snake;

    private void OnTriggerEnter(Collider other)
    {
        snake.SetActive(true);
    }
}
