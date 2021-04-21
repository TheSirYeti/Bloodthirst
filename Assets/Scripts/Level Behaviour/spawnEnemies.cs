using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour
{

    public GameObject enemyPrefab;
    public Transform spawnpoint1;
    public Transform spawnpoint2;
    public Transform spawnpoint3;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject enemy1 = Instantiate(enemyPrefab, spawnpoint1);
            GameObject enemy2 = Instantiate(enemyPrefab, spawnpoint2);
            GameObject enemy3 = Instantiate(enemyPrefab, spawnpoint3);
        }
    }
}
