using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        SoundManager.instance.PlayMusic(MusicID.MAIN_MENU, true, 1);
    }
}
