using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class skipIntro : MonoBehaviour
{
    public Animator fader;
    public VideoPlayer player;
    public float timeToWait;

    private void Start()
    {
        StartCoroutine(videoLenght());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            player.Pause();
            fader.SetTrigger("fade");
        }
    }

    IEnumerator videoLenght()
    {
        yield return new WaitForSeconds(timeToWait);
        fader.SetTrigger("fade");
    }

}
