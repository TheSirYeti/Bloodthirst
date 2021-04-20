using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera cameraSettings;
    private CinemachineVirtualCamera originalCameraSettings;
    public Vector3 newPosition;

    public GameObject enabledCamera;
    public GameObject transitionCamera;

    private void Start()
    {
        originalCameraSettings = cameraSettings;
    }

    private void OnTriggerEnter(Collider other)
    {
        enabledCamera.SetActive(false);
        transitionCamera.SetActive(true);
    }
}
