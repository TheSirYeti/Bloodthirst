using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShield : MonoBehaviour
{
    public float shieldHP;
    public float maxHP;
    public Material originalMat;
    public Material hurtMat;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "attackFX" || other.gameObject.tag == "specialAttackFX")
        {
            shieldHP--;
            if(shieldHP <= 0)
            {
                SoundManager.instance.PlaySound(SoundID.SHIELD_BREAK);
                gameObject.SetActive(false);
            } else StartCoroutine(changeMaterial());
        }
    }

    IEnumerator changeMaterial()
    {
        gameObject.GetComponent<MeshRenderer>().material = hurtMat;
        SoundManager.instance.PlaySound(SoundID.SHIELD_HURT);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<MeshRenderer>().material = originalMat;
    }

    public void resetValues()
    {
        shieldHP = maxHP;
    }
}
