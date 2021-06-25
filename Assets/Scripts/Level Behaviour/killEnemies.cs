using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killEnemies : MonoBehaviour
{
    public GameObject enemies;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(CheckpointBehaviour.instance.currentCheckpoint);
        if (CheckpointBehaviour.instance.currentCheckpoint == 10)
            enemies.SetActive(false);
    }
}
