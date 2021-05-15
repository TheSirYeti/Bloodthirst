using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSong : MonoBehaviour
{
    void Start()
    {
        SoundManager.instance.PlayMusic(MusicID.MAIN_MENU, true, 1);
    }
}
