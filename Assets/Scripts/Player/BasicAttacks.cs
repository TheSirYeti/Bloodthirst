using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Behaviour
{
    public class BasicAttacks : MonoBehaviour
    {
        public      float           attackCooldown                      = 0.3f;
        public      float           airAttackCooldown                   = 5f;
        public      float           timeToNextAttack                    = -1f;
        public      float           timeToNextAirAttack                 = -1f;
        public      float           timeToCombo                         = 0.4f;
        public      float           comboTimeRemaining                  = -1f;
        public      int             attackTurn                          = 0;
        public      int             maxCombo                            = 3;
        public      Animator        animator;
        public      RuntimeAnimatorController       dualWield;
        public GameObject dualsword1, dualsword2;
        public GameObject sword;
        public      RuntimeAnimatorController       bigSword;
        public int currentWeapon;
        public      string          attackButtonParameterName           = "Fire1";
        public      string          attackAnimatorBoolParameterName     = "isAttacking";
        public      string          attackAnimatorTriggerParameterName  = "attack";
        public      string          attackTurnAnimatorParameterName     = "attackTurn";
        public      Rigidbody       rigidBody;
        public      float           attackForce                         = 1f;
        public      GameObject      player;
        public      ForceMode       attackForceMode                     = ForceMode.Impulse;
        public      Collider        attackCollider;
        public      Collider        bigAttackCollider;
        private     float           colliderCooldown;
        private     float           colliderTime                        = 0.25f;
        private     bool            colliderBool;
        [SerializeField]private     float           attackTurnCooldown;
        private     float           attackTurnTime                      = 0.3f;

        private void Start()
        {

        }

        public void attack()
        {
            //attackTurn++;
            animator.SetTrigger(attackAnimatorTriggerParameterName);
            timeToNextAttack = attackCooldown + Time.time;
            StartCoroutine(enableCollider(0));
            //checkCombo();
        }

        public void heavyAttack()
        {
            animator.SetTrigger(attackAnimatorTriggerParameterName);
            timeToNextAttack = (attackCooldown * 8f) + Time.time;
            StartCoroutine(enableBigCollider(2.45f));
            //checkCombo();
        }

        public void airAttack()
        {
            colliderCooldown = colliderTime + Time.time;
            timeToNextAttack = airAttackCooldown + Time.time;
            comboTimeRemaining = timeToCombo + Time.time;
            animator.SetTrigger(attackAnimatorTriggerParameterName);
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            rigidBody.AddForce((player.transform.forward + player.transform.up) * attackForce, attackForceMode);
        }

        public bool isAttacking()
        {
            return animator.GetBool(attackAnimatorBoolParameterName);
        }

        public bool checkAttackCooldown()
        {
            if (timeToNextAttack < Time.time)
            {
                return true;   
            }
            else return false;
        }

        public void resetAttackTurn()
        {
            if((attackTurnCooldown < Time.time && attackTurn != 0) || attackTurn >= 3)
            {
                attackTurn = 0;
            }
        }

        public int getCurrentAttackTurn()
        {
            return attackTurn;
        }

        public void checkCombo()
        {
            StartCoroutine(comboTrigger());
        }

        IEnumerator enableCollider(float time)
        {
            yield return new WaitForSeconds(time);
            attackCollider.enabled = true;
            yield return new WaitForSeconds(0.5f);
            attackCollider.enabled = false;
        }

        public IEnumerator enableBigCollider(float time)
        {
            yield return new WaitForSeconds(time);
            bigAttackCollider.enabled = true;
            yield return new WaitForSeconds(0.5f);
            bigAttackCollider.enabled = false;
        }

        public void changeWeapons() {
            switch (currentWeapon)
            {
                case 0:
                    //animator.runtimeAnimatorController = bigSword;
                    dualsword1.SetActive(false);
                    dualsword2.SetActive(false);
                    sword.SetActive(true);
                    currentWeapon = 1;
                    break;
                case 1:
                    //animator.runtimeAnimatorController = dualWield;
                    dualsword1.SetActive(true);
                    dualsword2.SetActive(true);
                    sword.SetActive(false);
                    currentWeapon = 0;
                    break;
            }   
                
        }

        public IEnumerator comboTrigger()
        {
            animator.SetTrigger("combo");
            yield return new WaitForSeconds(0.75f);
            
            animator.ResetTrigger("combo");
        }

    }
} 
