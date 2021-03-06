using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float hp;
    public float speed;
    public Transform lookAtPoint;
    public bool vunerable = true;
    public ParticleSystem blood;
    public Material originalMat;

    private void Start()
    {
        originalMat = GetComponentInChildren<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {

        if ((other.gameObject.tag == "attackFX" || other.gameObject.tag == "specialAttackFX" || other.gameObject.tag == "kamikazeAttack") && vunerable)
        {
            Rigidbody body = GetComponent<Rigidbody>();
            if (body != null && hp > 0)
            {
                blood.Stop();
                blood.Play();
                SoundManager.instance.PlaySound(SoundID.BLOOD_1);
                EventManager.Trigger("AddSpecial", 0.005f);
                Vector3 difference = transform.position - other.transform.position;
                difference = difference.normalized * 20;
                body.AddForce(difference, ForceMode.Impulse);
                StartCoroutine(Knockback(body));
            }
            Debug.Log("damn thats crazy");
            takeDamage(1);
        }

        if(other.gameObject.tag == "heavyAttackFX")
        {
            Rigidbody body = GetComponent<Rigidbody>();
            if (body != null && hp > 0)
            {
                blood.Stop();
                blood.Play();
                SoundManager.instance.PlaySound(SoundID.BLOOD_1);
                EventManager.Trigger("AddSpecial", 0.01f);
                Vector3 difference = transform.position - other.transform.position;
                difference = difference.normalized * 20;
                body.AddForce(difference, ForceMode.Impulse);
                StartCoroutine(Knockback(body));
            }
            takeDamage(2);
        }

        if (other.gameObject.tag == "bigAttackFX")
        {
            blood.Stop();
            blood.Play();
            takeDamage(5);
        }
    }

    IEnumerator Knockback(Rigidbody body)
    {
      
        Material mat = GetComponentInChildren<Renderer>().material;
        
        if (body != null)
        {
            mat.color = new Color(1, 0, 0, 1);
            SoundManager.instance.PlaySound(SoundID.ENEMY_DAMAGE);
            yield return new WaitForSeconds(0.5f);
            mat.color = originalMat.color;
            body.velocity = Vector3.zero;
        }
    }

    public abstract void takeDamage(int amount);
}
