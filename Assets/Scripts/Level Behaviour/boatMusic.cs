using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatMusic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && gameObject.tag == "boat")
            SoundManager.instance.PlayMusic(MusicID.BOAT);
    }

    private void OnTriggerExit(Collider other)
    {
        SoundManager.instance.StopMusic(MusicID.BOAT);
    }
}
