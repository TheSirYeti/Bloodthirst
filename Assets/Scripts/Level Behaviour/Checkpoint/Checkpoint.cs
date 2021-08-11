using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointID;

    public GameObject checkpointMarker;
    public ParticleSystem checkpointParticles;

    private void Start()
    {
        if(CheckpointBehaviour.instance.currentCheckpoint >= checkpointID)
        {
            checkpointMarker.SetActive(true);
            checkpointParticles.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && checkpointID > CheckpointBehaviour.instance.GetCurrentCheckpoint())
        {
            CheckpointBehaviour.instance.SetCurrentCheckpoint(checkpointID);
            checkpointMarker.SetActive(true);
            checkpointParticles.Play();
            SoundManager.instance.PlaySound(SoundID.CHECKPOINT);
            EventManager.Trigger("AddHP", 10f);
            EventManager.Trigger("HealingEffect");
        }
    }
}
