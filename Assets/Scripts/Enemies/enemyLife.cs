using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLife : MonoBehaviour
{
    [SerializeField]protected int hp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "attackFX")
        {
            hp--;
        }
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Debug.Log("Entra");
            SoundManager.instance.Play(SoundID.AMOGUS, false, 0.3f, 1);
            Destroy(gameObject);
        }
    }
}
