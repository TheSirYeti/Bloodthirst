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
        public      GameObject dualsword1, dualsword2;
        public      GameObject sword;
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
        public Collider specialAttackCollider;
        public      Collider        bigAttackCollider;
        private     float           colliderCooldown;
        private     float           colliderTime                        = 0.25f;
        private     bool            colliderBool;
        [SerializeField]private     float           attackTurnCooldown;
        private     float           attackTurnTime                      = 0.3f;
        public bool isInvunerable;

        public void attack()
        {
            animator.SetTrigger(attackAnimatorTriggerParameterName);
            timeToNextAttack = attackCooldown + Time.time;
            StartCoroutine(enableCollider(0));
        }

        public void heavyAttack()
        {
            animator.SetTrigger(attackAnimatorTriggerParameterName);
            timeToNextAttack = attackCooldown * 1.5f + Time.time;
            //StartCoroutine(enableBigCollider(2.45f));
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
            //isInvunerable = true;
            attackCollider.enabled = true;
            yield return new WaitForSeconds(0.5f);
            //isInvunerable = false;
            attackCollider.enabled = false;
        }

        IEnumerator enableSpecialCollider(float time)
        {
            yield return new WaitForSeconds(time);
            //isInvunerable = true;
            specialAttackCollider.enabled = true;
            yield return new WaitForSeconds(0.5f);
            //isInvunerable = false;
            specialAttackCollider.enabled = false;
        }

        public IEnumerator enableBigCollider(float time)
        {
            yield return new WaitForSeconds(time);
            bigAttackCollider.enabled = true;
            //isInvunerable = true;
            yield return new WaitForSeconds(0.5f);
            //isInvunerable = false;
            bigAttackCollider.enabled = false;
        }

        public void changeWeapons() {
            switch (currentWeapon)
            {
                case 0:
                    animator.runtimeAnimatorController = bigSword;
                    dualsword1.SetActive(false);
                    dualsword2.SetActive(false);
                    sword.SetActive(true);
                    currentWeapon = 1;
                    break;
                case 1:
                    animator.runtimeAnimatorController = dualWield;
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
            yield return new WaitForSeconds(3f);
            animator.ResetTrigger("combo");
        }


        public void specialAttack()
        {
            animator.SetTrigger("specialAttack");
            switch (currentWeapon)
            {
                case 0:
                    StartCoroutine(spinning());
                    break;
            }
        }


        IEnumerator spinning()
        {
            animator.SetBool("specialAttackOver", false);
            //isInvunerable = true;
            yield return new WaitForSeconds(5f);
            //isInvunerable = false;
            animator.SetBool("specialAttackOver", true);
        }

        public IEnumerator charging()
        {
            isInvunerable = true;
            yield return new WaitForSeconds(1f);
            isInvunerable = false;
        }

        public void MakeInvunerable()
        {
            isInvunerable = true;
        }

        public void MakeVunerable()
        {
            isInvunerable = false;
        }
    }
} 
