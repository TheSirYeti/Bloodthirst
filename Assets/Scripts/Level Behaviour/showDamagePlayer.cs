using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Behaviour;

public class showDamagePlayer : MonoBehaviour
{
    bool isInDamage = false;
    bool isDashing = false;
    int currentWeapon;
    void Start()
    {
        EventManager.Subscribe("DamagePlayer", Take);
        EventManager.Subscribe("Dash", Vanish);
    }

    private void Update()
    {
        if (!isInDamage)
        {
            GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        }

        if (!isDashing)
        {
            GetComponent<Renderer>().enabled = true;
        }
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
            StartCoroutine(Dash());
        }
    }

    IEnumerator Damage()
    {
        isInDamage = true;
        GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
        SoundManager.instance.PlaySound(SoundID.PLAYER_DAMAGE);
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        isInDamage = false;
    }

    IEnumerator Dash()
    {
        isDashing = true;
        GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().enabled = true;
        isDashing = false;
    }
}
