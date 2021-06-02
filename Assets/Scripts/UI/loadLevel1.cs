using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel1 : MonoBehaviour
{
    public int index;
    public void loadScene()
    {
        Time.timeScale = 1f;
        SoundManager.instance.StopAllMusic();
        SoundManager.instance.StopAllSounds();
        SceneManager.LoadScene(index);
    }    
}
