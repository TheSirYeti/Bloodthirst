using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public Image image;
    public Canvas canvas;
    public Transform currentTarget;

    private void Update()
    {
        if(currentTarget != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(currentTarget.position);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0f, 0f, 1f);
    }

    public void setCrosshairPosition(Transform enemyPosition)
    {
        var wantedPos = Camera.main.WorldToScreenPoint(enemyPosition.position);
        transform.position = wantedPos;
        currentTarget = enemyPosition;
        enableImage();
    }

    public void enableImage()
    {
        image.enabled = true;
    }

    public void disableImage()
    {
        image.enabled = false;
    }
}
