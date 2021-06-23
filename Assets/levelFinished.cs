using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelFinished : MonoBehaviour
{
    public int level;

    public void load()
    {
        Time.timeScale = 1f;
        if (SoundManager.instance != null)
        {
            SoundManager.instance.StopAllMusic();
            SoundManager.instance.StopAllSounds();
        }

        if (CheckpointBehaviour.instance != null && SceneManager.GetActiveScene().buildIndex != level)
        {
            Debug.Log("HOLA LEVEL BORRATE");
            CheckpointBehaviour.instance.DestroyInstance();
            //Destroy(CheckpointBehaviour.instance);
        }
        SceneManager.LoadScene(level);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            load();
        }
    }

}
