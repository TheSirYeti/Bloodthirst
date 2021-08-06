using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public GameObject enemy;
    public Material originalMat;
    public ParticleSystem blood;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "attackFX")
        {
            enemy.GetComponent<EnemyBase>().ReceiveDamage(1);
            EventManager.Trigger("AddSpecial", 0.005f);
            Rigidbody body = enemy.GetComponent<Rigidbody>();
            blood.Stop();
            blood.Play();
            SoundManager.instance.PlaySound(SoundID.BLOOD_1);
            if (body != null && enemy.GetComponent<EnemyBase>().getlife() > 0)
            {
                Vector3 difference = enemy.transform.position - other.transform.position;
                difference = difference.normalized * 8;
                body.AddForce(difference, ForceMode.Impulse);
                StartCoroutine(Knockback(body));
            }
        }

        if (other.gameObject.tag == "heavyAttackFX")
        {
            enemy.GetComponent<EnemyBase>().ReceiveDamage(2);
            EventManager.Trigger("AddSpecial", 0.01f);
            Rigidbody body = enemy.GetComponent<Rigidbody>();
            blood.Stop();
            blood.Play();
            SoundManager.instance.PlaySound(SoundID.BLOOD_1);
            if (body != null && enemy.GetComponent<EnemyBase>().getlife() > 0)
            {
                Vector3 difference = enemy.transform.position - other.transform.position;
                difference = difference.normalized * 8;
                body.AddForce(difference, ForceMode.Impulse);
                StartCoroutine(Knockback(body));
            }
        }

        if (other.gameObject.tag == "bigAttackFX")
        {
            enemy.GetComponent<EnemyBase>().ReceiveDamage(10);
            Rigidbody body = enemy.GetComponent<Rigidbody>();
            blood.Stop();
            blood.Play();
            SoundManager.instance.PlaySound(SoundID.BLOOD_1);
            if (body != null && enemy.GetComponent<EnemyBase>().getlife() > 0)
            {
                Vector3 difference = enemy.transform.position - other.transform.position;
                difference = difference.normalized * 20;
                body.AddForce(difference, ForceMode.Impulse);
                StartCoroutine(Knockback(body));
            }
        }

        if(other.gameObject.tag == "specialAttackFX")
        {
            enemy.GetComponent<EnemyBase>().ReceiveDamage(1);
            Rigidbody body = enemy.GetComponent<Rigidbody>();
            blood.Stop();
            blood.Play();
            SoundManager.instance.PlaySound(SoundID.BLOOD_1);
            if (body != null && enemy.GetComponent<EnemyBase>().getlife() > 0)
            {
                Vector3 difference = enemy.transform.position - other.transform.position;
                difference = difference.normalized * 8;
                body.AddForce(difference, ForceMode.Impulse);
                StartCoroutine(Knockback(body));
            }
        }
    }

    IEnumerator Knockback(Rigidbody body)
    {
        Color c;
        Material mat = enemy.GetComponentInChildren<Renderer>().material;
        if (body != null)
        {
            c = mat.color;
            mat.color = new Color(1,0,0,1);
            SoundManager.instance.PlaySound(SoundID.ENEMY_DAMAGE);
            yield return new WaitForSeconds(0.5f);
            mat.color = originalMat.color;
            body.velocity = Vector3.zero;
        }
    }
}
