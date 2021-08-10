using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killEnemies : MonoBehaviour
{
    public GameObject enemies;
    public float kill;
    // Start is called before the first frame update
    void Start()
    {
        if (CheckpointBehaviour.instance.currentCheckpoint == kill)
            enemies.SetActive(false);
    }
}
