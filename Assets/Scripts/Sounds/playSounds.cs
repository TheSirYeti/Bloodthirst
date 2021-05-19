using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSounds : MonoBehaviour
{
    public void SFX_Step1()
    {
        SoundManager.instance.PlaySound(SoundID.STEP_1);
    }

    public void SFX_Step2()
    {
        SoundManager.instance.PlaySound(SoundID.STEP_2);
    }

    public void SFX_Jump()
    {
        SoundManager.instance.PlaySound(SoundID.JUMP);
    }

    public void SFX_SLASH1()
    {
        SoundManager.instance.PlaySound(SoundID.SWORD_SLASH1);
    }

    public void SFX_SLASH2()
    {
        SoundManager.instance.PlaySound(SoundID.SWORD_SLASH2);
    }

    public void SFX_SLASH3()
    {
        SoundManager.instance.PlaySound(SoundID.SWORD_SLASH3);
    }

    public void SFX_SLASH4()
    {
        SoundManager.instance.PlaySound(SoundID.SWORD_SLASH4);
    }

    public void SFX_SLASH5()
    {
        SoundManager.instance.PlaySound(SoundID.SWORD_SLASH5);
    }
}
