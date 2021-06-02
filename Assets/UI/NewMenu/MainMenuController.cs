using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    public GameObject buttonGroup, optionsPanel, creditsPanel, quitPanel;
    public ContinueInput fader;
    public GameObject mainMenuButton, creditsButton, optionsButton, controlsButton, soundButton, quitButton;

    public void viewOptions()
    {
        buttonGroup.SetActive(false);
        optionsPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(optionsButton);
    }

    public void closeOptions()
    {
        optionsPanel.SetActive(false);
        buttonGroup.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuButton);
    }

    public void viewCredits()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsButton);
        creditsPanel.SetActive(true);
        buttonGroup.SetActive(false);
    }

    public void closeCredits()
    {
        creditsPanel.SetActive(false);
        buttonGroup.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuButton);
    }

    public void viewQuit()
    {
        quitPanel.SetActive(true);
        buttonGroup.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quitButton);
    }

    public void closeQuit()
    {
        quitPanel.SetActive(false);
        buttonGroup.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuButton);
    }

    public void playGame()
    {
        SoundManager.instance.StopAllMusic();
        SoundManager.instance.StopAllSounds();
        fader.fade();
    }
}
