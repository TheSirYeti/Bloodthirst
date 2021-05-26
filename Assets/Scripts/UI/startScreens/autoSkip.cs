using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class autoSkip : MonoBehaviour
{
    public ContinueInput fade;

    private void Start()
    {
        StartCoroutine(Skip());
    }

    IEnumerator Skip()
    {
        yield return new WaitForSeconds(5f);
        fade.fade();
    }
}
