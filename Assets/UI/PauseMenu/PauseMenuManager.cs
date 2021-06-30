using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu, soundPanel, controlsPanel;
    public GameObject startButton;
    public GameObject pauseButton, controlsButton, soundButton;

    public PlayerController player;

    private void Start()
    {
        
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !player.deathPanel.activeSelf)
        {
            if (!pauseMenu.activeSelf)
            {
                openPanel();
                Time.timeScale = 0f;
                player.enabled = false;
            }
            else
            {
                player.enabled = true;
                closePanel();
            }
        }
    }

    public void openPanel()
    {
        SoundManager.instance.PauseAllSounds();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        soundPanel.SetActive(false);
        controlsPanel.SetActive(false);
        pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseButton);
        Time.timeScale = 0f;
    }

    public void closePanel()
    {
        SoundManager.instance.ResumeAllSounds();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        soundPanel.SetActive(false);
        controlsPanel.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        player.enabled = true;
    }

    public void viewSound()
    {
        pauseMenu.SetActive(false);
        soundPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(soundButton);
    }

    public void closeSound()
    {
        pauseMenu.SetActive(true);
        soundPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseButton);
    }

    public void viewControls()
    {
        pauseMenu.SetActive(false);
        controlsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlsButton);
    }

    public void closeControls()
    {
        pauseMenu.SetActive(true);
        controlsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseButton);
    }
}
