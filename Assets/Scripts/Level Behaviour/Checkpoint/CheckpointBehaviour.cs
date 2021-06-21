using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehaviour : MonoBehaviour
{
    List<Transform> checkpoints = new List<Transform>();

    int currentCheckpoint;

    public void SetCurrentCheckpoint(int index)
    {
        if (checkpoints[index] != null)
            currentCheckpoint = index;
    }

    public int GetCurrentCheckpoint()
    {
        return currentCheckpoint;
    }


}
