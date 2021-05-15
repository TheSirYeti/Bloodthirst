using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public Slider sliderSFX;
    public Slider sliderMusic;

    private void Start()
    {
        sliderSFX.value = SoundManager.instance.volumeSFX;
        sliderMusic.value = SoundManager.instance.volumeMusic;
    }

    private void Update()
    {
        if(SoundManager.instance.volumeSFX != sliderSFX.value)
        {
            SoundManager.instance.ChangeVolumeSound(sliderSFX.value);
        }

        if (SoundManager.instance.volumeMusic != sliderMusic.value)
        {
            SoundManager.instance.ChangeVolumeMusic(sliderMusic.value);
        }
    }
}
