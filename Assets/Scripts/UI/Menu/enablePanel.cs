using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enablePanel : MonoBehaviour
{
    public GameObject block;

    public void showPanel()
    {
        SoundManager.instance.PlaySound(SoundID.SELECT, false, 1);
        gameObject.SetActive(true);
        block.SetActive(true);
    }

    public void hidePanel()
    {
        SoundManager.instance.PlaySound(SoundID.SELECT, false, 1);
        gameObject.SetActive(false);
        block.SetActive(false);
    }

    public void quitGame()
    {
        SoundManager.instance.PlaySound(SoundID.SELECT, false, 1);
        Application.Quit();
    }
}
