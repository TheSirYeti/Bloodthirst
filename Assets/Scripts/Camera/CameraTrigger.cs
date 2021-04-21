using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera cameraSettings;
    private CinemachineVirtualCamera originalCameraSettings;
    public Vector3 newPosition;

    public GameObject enabledCamera = null;
    public GameObject transitionCamera;

    private void Start()
    {
        originalCameraSettings = cameraSettings;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(enabledCamera != null)
        {
            enabledCamera.SetActive(false);
        }
        transitionCamera.SetActive(true);
    }
}
