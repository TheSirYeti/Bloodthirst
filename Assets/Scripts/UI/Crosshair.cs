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
        StartCoroutine(enableImage());
    }

    public IEnumerator enableImage()
    {
        image.enabled = true;
        yield return new WaitForSeconds(2);
        image.enabled = false;
    }
}
