using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueInput : MonoBehaviour
{

    public Animator animator;
    public bool allowed;

    private void Start()
    {
        EventManager.Subscribe("EndFade", fadeProgram);
    }
    void Update()
    {
        if (Input.GetButtonDown("Start") && allowed)
        {
            fadeProgram(null); 
        }
    }

    public void fadeProgram(object[] parameters)
    {
        animator.SetTrigger("fade");
        if(allowed)
            SoundManager.instance.PlaySound(SoundID.SELECT, false, 1);
    }

    public void fade()
    {
        animator.SetTrigger("fade");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
