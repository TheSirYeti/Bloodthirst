using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDamage : MonoBehaviour
{
    public GameObject boss;
    public Material originalMaterial;
    public Color hurtMaterial;
    private void Start()
    {
        hurtMaterial = Color.red;
        EventManager.Subscribe("HurtBoss", Hurt);
    }

    void Hurt(object[] parameters)
    {
        StartCoroutine(changeColor());
    }

    IEnumerator changeColor()
    {
        boss.GetComponent<SkinnedMeshRenderer>().material.color = hurtMaterial;
        yield return new WaitForSeconds(0.33f);
        boss.GetComponent<SkinnedMeshRenderer>().material = originalMaterial;
    }
}
