using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player.Behaviour;

public class Teleport : MonoBehaviour
{
    public Transform destination;
    public int currentLevel;
    public Cinemachine.CinemachineFreeLook camera;

    public Transform deathPosition;

    private void Awake()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SoundManager.instance.PlaySound(SoundID.SNAKE_DEATH);
            deathPosition.position = other.transform.position;
            camera.Follow = deathPosition;
            other.gameObject.GetComponentInChildren<PlayerLife>().hp = -10;
            //StartCoroutine(reloadScene());
            //other.transform.position = destination.position;
        }
    }

    IEnumerator reloadScene()
    {
        yield return new WaitForSeconds(0.25f);
        SoundManager.instance.PlaySound(SoundID.SNAKE_DEATH);
        yield return new WaitForSeconds(0.75f);
        SoundManager.instance.StopAllSounds();
        SoundManager.instance.StopAllMusic();
        SceneManager.LoadScene(currentLevel);
    }
}
