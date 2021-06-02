using System.Collections;
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
            fadeProgram(); 
        }
    }

    public void fadeProgram()
    {
        animator.SetTrigger("fade");
        SoundManager.instance.PlaySound(SoundID.SELECT, false, 1);
    }

    public void fade()
    {
        animator.SetTrigger("fade");
    }
}
