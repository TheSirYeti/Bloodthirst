using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueText : MonoBehaviour
{
    CanvasGroup _canvasGroup;
    float alphaValue = -0.015f;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(toggleObject());
    }
  

    IEnumerator toggleObject()
    {
        while (true)
        {
            if (_canvasGroup.alpha >= 1f || _canvasGroup.alpha <= 0)
                alphaValue = -alphaValue;

            gameObject.GetComponent<CanvasGroup>().alpha += alphaValue;
            yield return new WaitForSeconds(0.01f);
        }
    }

}
