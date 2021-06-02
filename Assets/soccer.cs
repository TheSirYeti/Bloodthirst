using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soccer : MonoBehaviour
{
    public ContinueInput fade;
    public void playmusic()
    {
        SoundManager.instance.PlayMusic(MusicID.SOCCER);
    }

    public void load()
    {
        fade.animator.SetTrigger("fade");
    }
}
