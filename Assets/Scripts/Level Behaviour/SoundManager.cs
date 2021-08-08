using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sounds;
    public AudioClip[] music;
    public AudioSource[] sfxChannel;
    public AudioSource[] musicChannel;

    public float volumeSFX;
    public float volumeMusic;

    public static SoundManager instance;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        sfxChannel = new AudioSource[sounds.Length];
        for (int i = 0; i < sfxChannel.Length; i++)
        {
            sfxChannel[i] = gameObject.AddComponent<AudioSource>();
            sfxChannel[i].clip = sounds[i];
        }

        musicChannel = new AudioSource[music.Length];
        for (int i = 0; i < musicChannel.Length; i++)
        {
            musicChannel[i] = gameObject.AddComponent<AudioSource>();
            musicChannel[i].clip = music[i];
        }

    }

    

    public void PlaySound(SoundID id, bool loop = false, float pitch = 1)
    {
        sfxChannel[(int)id].Play();
        sfxChannel[(int)id].loop = loop;
        sfxChannel[(int)id].volume = volumeSFX;
        sfxChannel[(int)id].pitch = pitch;
    }

    public void StopAllSounds()
    {
        for (int i = 0; i < sfxChannel.Length; i++)
        {
            sfxChannel[i].Stop();
        }
    }

    public void PauseAllSounds()
    {
        for (int i = 0; i < sfxChannel.Length; i++)
        {
            sfxChannel[i].Pause();
        }
    }

    public void ResumeAllSounds()
    {
        for (int i = 0; i < sfxChannel.Length; i++)
        {
            sfxChannel[i].UnPause();
        }
    }

    public void StopSound(SoundID id)
    {
        sfxChannel[(int)id].Stop();
    }

    public void PauseSound(SoundID id)
    {
        sfxChannel[(int)id].Pause();
    }

    public void ResumeSound(SoundID id)
    {
        sfxChannel[(int)id].UnPause();
    }

    public void ToggleMuteSound(SoundID id)
    {
        sfxChannel[(int)id].mute = !sfxChannel[(int)id].mute;
    }

    public void ChangeVolumeSound(float volume)
    {
        volumeSFX = volume;
        for (int i = 0; i < sfxChannel.Length; i++)
        {
            sfxChannel[i].volume = volumeSFX;
        }
    }



    public void PlayMusic(MusicID id, bool loop = false, float pitch = 1)
    {
        musicChannel[(int)id].Play();
        musicChannel[(int)id].loop = loop;
        musicChannel[(int)id].volume = volumeMusic;
        musicChannel[(int)id].pitch = pitch;
    }

    public void StopAllMusic()
    {
        for (int i = 0; i < musicChannel.Length; i++)
        {
            musicChannel[i].Stop();
        }
    }

    public void PauseAllMusic()
    {
        for (int i = 0; i < musicChannel.Length; i++)
        {
            musicChannel[i].Pause();
        }
    }

    public void ResumeAllMusic()
    {
        for (int i = 0; i < musicChannel.Length; i++)
        {
            musicChannel[i].UnPause();
        }
    }

    public void StopMusic(MusicID id)
    {
        musicChannel[(int)id].Stop();
    }

    public void PauseMusic(MusicID id)
    {
        musicChannel[(int)id].Pause();
    }

    public void ResumeMusic(MusicID id)
    {
        musicChannel[(int)id].UnPause();
    }

    public void ToggleMuteMusic(MusicID id)
    {
        musicChannel[(int)id].mute = !musicChannel[(int)id].mute;
    }

    public void ChangeVolumeMusic(float volume)
    {
        volumeMusic = volume;
        for (int i = 0; i < musicChannel.Length; i++)
        {
            musicChannel[i].volume = volumeMusic;
        }
    }
}

public enum SoundID
{
    SWORD_SLASH1,
    SWORD_SLASH2,
    SWORD_SLASH3,
    SWORD_SLASH4,
    SWORD_SLASH5,
    JUMP,
    RUN,
    CHANGE_SWORD,
    EXPLOSION1,
    SELECT,
    GUHHUH,
    STEP_1,
    STEP_2,
    STEP_3,
    CHARGELASER,
    RELEASE_FIRE,
    BURN,
    GROAN1,
    GROAN2,
    GROAN3,
    ATTACK,
    ENEMY_DAMAGE,
    SPIN,
    ENEMY_DEATH,
    PLAYER_DAMAGE,
    KAMIKAZE,
    SHIELD_HURT,
    SHIELD_BREAK,
    BOSS_VOICE1,
    BOSS_VOICE2,
    BOSS_VOICE3,
    KAMIKAZE_BUILDUP,
    GONG,
    KAZAN_LAUGH,
    CHECKPOINT,
    BLOOD_1,
    BLOOD_2,
    WOOSH,
    ACID_BURN,
    ACID_GURGLE,
    SNAKE_ATTACK_1,
    SNAKE_ATTACK_2,
    SNAKE_HIDE,
    SNAKE_APPEAR,
    SNAKE_HISS,
    SNAKE_SHIELDBREAK,
    SNAKE_RAGE,
    SNAKE_DEATH,
    BONES,
    LIGHTNING,
    HEAVY_SLASH1,
    HEAVY_SLASH2,
    HEAVY_SLASH3,
    BOSS_HIT
}

public enum MusicID
{
    MAIN_MENU,
    INTRO,
    SOCCER,
    NEW_MENU,
    BOAT,
    MAIN_SONG
}