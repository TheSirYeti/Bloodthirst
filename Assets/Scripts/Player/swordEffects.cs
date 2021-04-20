using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordEffects : MonoBehaviour
{
    public TrailRenderer trailRenderer;

    private float trailTime = 1f;
    private float timeRemaining;

    private void Start()
    {
        trailRenderer.emitting = false;
    }

    private void FixedUpdate()
    {
        if(timeRemaining < Time.time)
        {
            trailRenderer.emitting = false;
        }
    }
    public void toggleTrail()
    {
        trailRenderer.emitting = true;
        timeRemaining = trailTime + Time.time;
    }
}
