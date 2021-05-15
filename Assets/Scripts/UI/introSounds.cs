using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introSounds : MonoBehaviour
{
    public GameObject text;
    public ContinueInput fade;

    public void introMusic()
    {
        SoundManager.instance.PlayMusic(MusicID.INTRO, false, 1);
    }
    
    public void introSound()
    {
        SoundManager.instance.PlaySound(SoundID.GUHHUH, false, 1);
    }

    public void showText()
    {
        text.SetActive(true);
    }

    public void load()
    {
        fade.animator.SetTrigger("fade");
    }
}
