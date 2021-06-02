using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public GameObject controlsPanel;
    public GameObject soundPanel;

    public void viewSoundPanel()
    {
        soundPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void viewControlsPanel()
    {
        soundPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void enablePanel()
    {
        gameObject.SetActive(true);
    }

    public void disablePanel()
    {
        gameObject.SetActive(false);
    }
}
