using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSnakeBoss : MonoBehaviour
{

    private void Start()
    {
        if(CheckpointBehaviour.instance.currentCheckpoint == 15)
        {
            snake.SetActive(true);
            snakeUI.SetActive(true);
        }
    }
    public GameObject snake;
    public GameObject snakeUI;
    private void OnTriggerEnter(Collider other)
    {
        snake.SetActive(true);
        snakeUI.SetActive(true);
    }
}
