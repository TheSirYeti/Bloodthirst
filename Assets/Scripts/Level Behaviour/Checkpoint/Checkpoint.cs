using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointID;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && checkpointID > CheckpointBehaviour.instance.GetCurrentCheckpoint())
            CheckpointBehaviour.instance.SetCurrentCheckpoint(checkpointID);
    }
}
