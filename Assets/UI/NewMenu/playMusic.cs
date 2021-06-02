using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMusic : MonoBehaviour
{
    private void Start()
    {
        SoundManager.instance.StopAllMusic();
        SoundManager.instance.StopAllSounds();
        SoundManager.instance.PlayMusic(MusicID.NEW_MENU);
    }
}
