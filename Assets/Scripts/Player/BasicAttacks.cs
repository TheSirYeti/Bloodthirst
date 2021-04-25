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
                timeToNextAttack = attackCooldown + Time.time;
                attackTurn++;
            }
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

        public int getCurrentAttackTurn()
        {
            return attackTurn;
        }

        public void checkCombo()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag("NoAttack") || attackTurn >= 3)
            {
                attackTurn = 0;
                timeToNextAttack = Time.time + (attackCooldown * 2f);
                //animator.SetTrigger("attack");
            }
        }
    }
} 
