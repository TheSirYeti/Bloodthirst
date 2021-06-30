using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killEnemies : MonoBehaviour
{
    public GameObject enemies;
    // Start is called before the first frame update
    void Start()
    {
        if (CheckpointBehaviour.instance.currentCheckpoint == 16)
            enemies.SetActive(false);
    }
}
