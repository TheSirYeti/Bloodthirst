using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControls : MonoBehaviour
{
    public GameObject gamepadPanel;
    public GameObject keyboardPanel;

    public void showGamepad()
    {
        gamepadPanel.SetActive(true);
        keyboardPanel.SetActive(false);
    }

    public void showKeyboard()
    {
        gamepadPanel.SetActive(false);
        keyboardPanel.SetActive(true);
    }
}
