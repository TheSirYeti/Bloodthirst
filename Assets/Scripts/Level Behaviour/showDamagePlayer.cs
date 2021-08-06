using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showDamagePlayer : MonoBehaviour
{   

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Subscribe("DamagePlayer", Take);
        EventManager.Subscribe("Dash", Vanish);
    }

    void Take(object[] parameters)
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(Damage());
        }
    }

    void Vanish(object[] parameters)
    {
        if (gameObject.activeSelf)
        {
            StopCoroutine(Damage());
            StartCoroutine(Dash());
        }
    }

    IEnumerator Damage()
    {
        GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
        SoundManager.instance.PlaySound(SoundID.PLAYER_DAMAGE);
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
    }

    IEnumerator Dash()
    {
        GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().enabled = true;
    }
}
