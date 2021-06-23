using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public GameObject kamikazePrefab;
    public Transform spawnPosition;
    public Animator animator;

    private void Update()
    {
        
    }

    public void WalkToPlayer()
    {

    }

    public void spawnKamikazes()
    {
        StartCoroutine(RandomArea());
    }

    IEnumerator RandomArea()
    {
        bool flag = true;
        int counter = 4;

        while (flag)
        {
            float xPos = Random.Range(-7, 8);
            float zPos = Random.Range(-7, 8);

            GameObject kamikaze = Instantiate(kamikazePrefab);
            kamikaze.transform.position = new Vector3(spawnPosition.position.x + xPos, spawnPosition.position.y, spawnPosition.position.z + zPos);

            yield return new WaitForSeconds(1);

            counter--;

            if(counter <= 0)
            {
                flag = false;
            }
        }
    }
}
