using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Behaviour
{
    public class BasicAttacks : MonoBehaviour
    {
        public      float           attackCooldown                      = 0.1f;
        public      float           airAttackCooldown                   = 5f;
        public      float           timeToNextAttack                    = -1f;
        public      float           timeToNextAirAttack                 = -1f;
        public      float           timeToCombo                         = 0.4f;
        public      float           comboTimeRemaining                  = -1f;
        public      int             attackTurn                          = 0;
        public      int             maxCombo                            = 3;
        public      Animator        animator;
        public      string          attackButtonParameterName           = "Fire1";
        public      string          attackAnimatorBoolParameterName     = "isAttacking";
        public      string          attackAnimatorTriggerParameterName  = "attack";
        public      string          attackTurnAnimatorParameterName     = "attackTurn";
        public      Rigidbody       rigidBody;
        public      float           attackForce                         = 1f;
        public      GameObject      player;
        public      ForceMode       attackForceMode                     = ForceMode.Impulse;
        public      Collider        attackCollider;
        private     float           colliderCooldown;
        private     float           colliderTime                        = 0.25f;


        public void attack()
        {
            if (timeToNextAttack < Time.time)
            {
                animator.SetTrigger(attackAnimatorTriggerParameterName);
                attackCollider.enabled = true;
                colliderCooldown = colliderTime + Time.time;
            }
        }

        public void airAttack()
        {
            attackCollider.enabled = true;
            colliderCooldown = colliderTime + Time.time;
            timeToNextAttack = airAttackCooldown + Time.time;
            comboTimeRemaining = timeToCombo + Time.time;
            animator.SetTrigger(attackAnimatorTriggerParameterName);
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
            rigidBody.AddForce((player.transform.forward + player.transform.up) * attackForce, attackForceMode);
        }

        public void checkCombo()
        {
            if (comboTimeRemaining < Time.time)
            {
                animator.SetBool(attackAnimatorBoolParameterName, false);
                attackTurn = 0;
                animator.SetInteger(attackTurnAnimatorParameterName, attackTurn);
            }
        }

        public bool isAttacking()
        {
            return animator.GetBool(attackAnimatorBoolParameterName);
        }

        public bool checkAirAttackCooldown()
        {
            if (timeToNextAttack < Time.time)
            {
                return true;
            }
            else return false;
        }

        private void FixedUpdate()
        {
            if(colliderCooldown < Time.time)
            {
                attackCollider.enabled = false;
            }
        }
    }
} 
