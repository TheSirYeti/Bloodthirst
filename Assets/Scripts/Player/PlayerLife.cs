using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public float hp = 10;
    public bool amIHurt;
    public bool hurtAllowed;
    public Rigidbody rigidbody;
    public GameObject material;
    public Slider hpBar;
    public PlayerController controller;
    Material originalMaterial;
    

    private void Start()
    {
        originalMaterial = material.GetComponent<Renderer>().material;
        EventManager.Subscribe("AddHP", setHP);
    }

    private void Update()
    {
        hpBar.value = hp;
        EventManager.Trigger("SetHP", hp / 10);
        if(hp <= 0)
        {
            EventManager.Trigger("KillPlayer");
            controller.movement.animator.SetTrigger("dead");
            controller.movement.enabled = false;
            controller.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "enemyFireAttack" && !controller.movement.isAttacking && !controller.basicAttacks.isInvunerable)
        {
            Destroy(other.gameObject);
            hp--;
            StartCoroutine(Knockback(rigidbody));
        }
    }

    public float getHP()
    {
        return hp;
    }

    public void setHP(object[] parameters)
    {
        Debug.Log((float)parameters[0]);
        hp = (float)parameters[0];
        Debug.Log("BOCA");
        EventManager.Trigger("SetHP", hp / 10);
    }

    public void modifyHP(float value, bool sign)
    {
        if (sign)
            hp += value;
        else hp -= value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemyAttack" && !controller.movement.isAttacking && !controller.basicAttacks.isInvunerable)
        {
            Vector3 difference = transform.position - other.transform.position;
            difference = difference.normalized * 100;
            rigidbody.AddForce(difference, ForceMode.Impulse);
            hp--;
            EventManager.Trigger("SetHP", hp / 10);
            //StartCoroutine(Knockback(rigidbody));
            EventManager.Trigger("DamagePlayer");
        }

        if (other.gameObject.tag == "kamikazeAttack" && !controller.basicAttacks.isInvunerable)
        {
            Vector3 difference = transform.position - other.transform.position;
            difference = difference.normalized * 100;
            rigidbody.AddForce(difference, ForceMode.Impulse);
            hp -= 3.5f;
            EventManager.Trigger("SetHP", hp / 10);
            //StartCoroutine(Knockback(rigidbody));
            EventManager.Trigger("DamagePlayer");
        }

        if (other.gameObject.tag == "enemyFireAttack" && !controller.movement.isAttacking && !controller.basicAttacks.isInvunerable)
        {
            Destroy(other.gameObject);
            hp--;
            EventManager.Trigger("SetHP", hp / 10);
            //StartCoroutine(Knockback(rigidbody));
            EventManager.Trigger("DamagePlayer");
        }

        if (other.gameObject.tag == "spikeTrap" && !controller.basicAttacks.isInvunerable)
        {
            Vector3 difference = transform.position - other.transform.position;
            difference = difference.normalized * 100;
            rigidbody.AddForce(difference, ForceMode.Impulse);
            hp--;
            EventManager.Trigger("SetHP", hp / 10);
            //StartCoroutine(Knockback(rigidbody));
            EventManager.Trigger("DamagePlayer");
        }

        if (other.gameObject.tag == "lightBossAttack" && !controller.basicAttacks.isInvunerable)
        {
            Vector3 difference = transform.position - other.transform.position;
            difference = difference.normalized * 200;
            rigidbody.AddForce(difference, ForceMode.Impulse);
            hp--;
            EventManager.Trigger("SetHP", hp / 10);
            //StartCoroutine(Knockback(rigidbody));
            EventManager.Trigger("DamagePlayer");
        }

        if (other.gameObject.tag == "snakeEnemyAttack" && !controller.basicAttacks.isInvunerable)
        {
            Vector3 difference = transform.position - other.transform.position;
            difference = difference.normalized * 200;
            //rigidbody.AddForce(difference, ForceMode.Impulse);
            hp -= 0.25f;
            EventManager.Trigger("SetHP", hp / 10);
            //StartCoroutine(Knockback(rigidbody));
            EventManager.Trigger("DamagePlayer");
        }

        if (other.gameObject.tag == "skeletonMelee" && !controller.basicAttacks.isInvunerable)
        {
            Vector3 difference = transform.position - other.transform.position;
            difference = difference.normalized * 100;
            rigidbody.AddForce(difference, ForceMode.Impulse);
            hp -= 0.3f;
            EventManager.Trigger("SetHP", hp / 10);
            //StartCoroutine(Knockback(rigidbody));
            EventManager.Trigger("DamagePlayer");
        }
    }

    IEnumerator hurtingWindow()
    {
        amIHurt = true;
        yield return new WaitForSeconds(1f);
        amIHurt = false;
    }

    IEnumerator Knockback(Rigidbody body)
    {
        
        if (body != null)
        {
            material.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
            SoundManager.instance.PlaySound(SoundID.PLAYER_DAMAGE);
            yield return new WaitForSeconds(0.5f);
            material.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
            body.velocity = Vector3.zero;
        }
    }
}
