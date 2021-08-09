using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour
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
            CheckpointBehaviour.instance.DestroyInstance();
            //Destroy(CheckpointBehaviour.instance);
        }
        SceneManager.LoadSceneAsync(level);
    }

    public void LoadAndResetCheckpoint()
    {
        if(CheckpointBehaviour.instance.currentCheckpoint != null)
        {
            CheckpointBehaviour.instance.currentCheckpoint = 0;
        }
        SceneManager.LoadSceneAsync(level);
    }
}
