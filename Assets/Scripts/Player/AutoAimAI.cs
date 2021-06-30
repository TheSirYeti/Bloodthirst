using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimAI : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> enemies;

    private void Start()
    {
        EventManager.Subscribe("AutoAim", lookAtEnemy);
    }

    public void lookAtEnemy(object[] parameters)
    {
        checkForNull();
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject g in enemies)
        {
            float enemyDistance = Vector3.Distance(player.transform.position, g.transform.position);
            if (enemyDistance < closestDistance)
            {
                closestEnemy = g;
                closestDistance = enemyDistance;
            }
        }

        if(closestEnemy != null)
        {
            player.transform.LookAt(new Vector3(closestEnemy.transform.position.x, player.transform.position.y, closestEnemy.transform.position.z));
        }
    }

    public void checkForNull()
    {
        List<GameObject> aux = new List<GameObject>();
        foreach (GameObject g in enemies)
        {
            if (g.gameObject != null && isInRange(g))
            {
                aux.Add(g);
            }
        }
        enemies = aux;
    }

    private bool isInRange(GameObject g)
    {
        float distance = (g.transform.position - transform.position).magnitude;
        if (distance >= 5f)
            return false;
        else return true;
    }

    public void addEnemy(GameObject enemy)
    {
        bool flag = false;
        foreach (GameObject g in enemies)
        {
            if (g == enemy)
            {
                flag = true;
            }
        }
        if (flag == false)
        {
            enemies.Add(enemy);
        }
    }

    public void removeEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
            addEnemy(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
            addEnemy(other.gameObject);
    }
}
