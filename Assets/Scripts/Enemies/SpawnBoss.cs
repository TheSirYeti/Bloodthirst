using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject boss;
    public GameObject wall;
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "attackFX")
        {
            SoundManager.instance.PlaySound(SoundID.GONG);
            wall.SetActive(true);
            boss.SetActive(true);
            gameObject.GetComponent<SpawnBoss>().enabled = false;
        }
    }
}
