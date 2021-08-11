using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehaviour : MonoBehaviour
{
    public List<Transform> checkpoints = new List<Transform>();
    public int currentCheckpoint;
    public int currentLevel;
    public static CheckpointBehaviour instance;

    private void Awake()
    {
        if (instance == null || instance.currentLevel != currentLevel)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(instance == null)
        {
            Destroy(gameObject);
        }
    }

    public void SetCurrentCheckpoint(int index)
    {
        if (checkpoints[index] != null)
            currentCheckpoint = index;
    }

    public int GetCurrentCheckpoint()
    {
        return currentCheckpoint;
    }

    public Transform GetCurrentSpawnpoint()
    {
        return checkpoints[currentCheckpoint];
    }

    public void changeCheckpoints(List<Transform> newCheckpoints)
    {
        checkpoints = newCheckpoints;
    }

    public void resetCurrentCheckpoint()
    {
        currentCheckpoint = 0;
    }

    public void DestroyInstance()
    {
        Destroy(gameObject);
    }
}
