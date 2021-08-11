using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSnakeBoss : MonoBehaviour
{
    public GameObject snake;
    public GameObject snakeUI;

    private void Start()
    {
        if(CheckpointBehaviour.instance.currentCheckpoint == 15)
        {
            snake.SetActive(true);
            snakeUI.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Sexo");
            snake.SetActive(true);
            snakeUI.SetActive(true);
        }
    }
}
