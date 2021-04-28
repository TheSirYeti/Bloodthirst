using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    public Vector3 newPosition;

    public GameObject enabledCamera = null;
    public GameObject transitionCamera;

    public bool changeDimension;
    public PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if(enabledCamera != null)
        {
            enabledCamera.SetActive(false);
        }
        transitionCamera.SetActive(true);

        player.movement.TwoDimensionMovement = changeDimension;
    }
}
