using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    private string lockButtonName = "LockButton";
    private string toggleLockButtonName = "ToggleLock";
    public int index = 0;
    public bool isLocked = false;

    public List<GameObject> enemiesInRange = new List<GameObject>();

    private void Update()
    {
        List<GameObject> aux = new List<GameObject>();
        foreach (GameObject g in enemiesInRange)
        {
            if (g.gameObject != null && isInRange(g))
            {
                aux.Add(g);
            }
        }
        enemiesInRange = aux;
    }

    public void addEnemy(GameObject enemy)
    {
        bool flag = false;
        foreach(GameObject g in enemiesInRange) 
        {
            if (g == enemy)
            {
                flag = true;
            }
        }
        if(flag == false)
        {
            enemiesInRange.Add(enemy);
        }
    }

    public void removeEnemy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    public GameObject toggleEnemy()
    {
        if (index >= enemiesInRange.Count - 1)
            index = 0;
        else index++;
        return enemiesInRange[index];
    }

    public GameObject currentEnemy()
    {
        return enemiesInRange[index];
    }

    public bool checkEnemiesAround()
    {
        if (enemiesInRange != null)
            return true;
        else return false;
    }

    private bool isInRange(GameObject g)
    {
        float distance = (g.transform.position - transform.position).magnitude;
        if (distance >= 25f)
            return false;
        else return true;
    }
}
