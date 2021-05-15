﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueInput : MonoBehaviour
{

    public Animator animator;
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            SoundManager.instance.PlaySound(SoundID.SELECT, false, 1);
            animator.SetTrigger("fade");   
        }
    }
}