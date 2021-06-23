using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public Transform destination;
    public int currentLevel;

    private void Awake()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(reloadScene());
            //other.transform.position = destination.position;
        }
    }

    IEnumerator reloadScene()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.instance.StopAllSounds();
        SoundManager.instance.StopAllMusic();
        SceneManager.LoadScene(currentLevel);
    }
}
