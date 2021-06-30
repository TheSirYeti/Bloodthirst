using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCam : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Fade());
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(0, 0.005f, -0.01f);
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.instance.PlaySound(SoundID.KAZAN_LAUGH);
        yield return new WaitForSeconds(6f);
        Debug.Log("CHAU");
        EventManager.Trigger("EndFade");
    }
}
