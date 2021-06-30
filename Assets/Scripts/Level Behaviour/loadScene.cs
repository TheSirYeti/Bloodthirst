using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    public int level;

    public void load()
    {
        Time.timeScale = 1f;
        if(SoundManager.instance != null)
        {
            SoundManager.instance.StopAllMusic();
            SoundManager.instance.StopAllSounds();
        }

        if(CheckpointBehaviour.instance != null && SceneManager.GetActiveScene().buildIndex == level)
        {
            Destroy(CheckpointBehaviour.instance);
        }
        EventManager.resetEventDictionary();
        SceneManager.LoadScene(level);
    }
}
