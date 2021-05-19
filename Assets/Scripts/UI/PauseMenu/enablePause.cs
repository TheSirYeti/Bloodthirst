using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enablePause : MonoBehaviour
{
    public GameObject panel;
    [SerializeField]float _timeCooldown;

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!panel.activeSelf)
            {
                openPanel();
            }
            else
            {
                closePanel();
            }
        }
    }

    public void openPanel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        panel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void closePanel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
}
