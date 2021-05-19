using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    void Start()
    {
        SoundManager.instance.PlaySound(SoundID.EXPLOSION1);
    }
}
