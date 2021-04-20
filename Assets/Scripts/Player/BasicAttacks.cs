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

        public void attack()
        {        
            if (timeToNextAttack < Time.time)
                attackAnimation();
            //else animator.SetBool(attackAnimatorBoolParameterName, false);
        }

        public void airAttack()
        {
                timeToNextAttack = airAttackCooldown + Time.time;
                comboTimeRemaining = timeToCombo + Time.time;
                animator.SetTrigger(attackAnimatorTriggerParameterName);
                rigidBody.velocity = Vector3.zero;
                rigidBody.angularVelocity = Vector3.zero;
                rigidBody.AddForce((player.transform.forward + player.transform.up) * attackForce, attackForceMode);
        }

        void attackAnimation()
        {
            animator.SetBool(attackAnimatorBoolParameterName, true);
            animator.SetTrigger(attackAnimatorTriggerParameterName);
            animator.SetInteger(attackTurnAnimatorParameterName, attackTurn);
            switch (attackTurn)
            {
                case 0:
                    moveAttack();
                    timeToNextAttack = attackCooldown + Time.time;
                    comboTimeRemaining = timeToCombo + Time.time;
                    attackTurn++;
                    break;
                case 1:
                    moveAttack();
                    timeToNextAttack = attackCooldown + Time.time;
                    comboTimeRemaining = timeToCombo + Time.time;
                    attackTurn++;
                    break;
                case 2:
                    moveAttack();
                    timeToNextAttack = attackCooldown + Time.time;
                    comboTimeRemaining = timeToCombo + Time.time;
                    attackTurn = 0;
                    break;
            }
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

        void moveAttack()
        {
            rigidBody.velocity = new Vector3(0f, 0f, 0f);
            //rigidBody.AddForce(player.transform.forward * attackForce, attackForceMode);
        }

        public bool checkAirAttackCooldown()
        {
            if (timeToNextAttack < Time.time)
            {
                return true;
            }
            else return false;
        }
    }
} 
