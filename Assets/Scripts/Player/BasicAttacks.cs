using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Behaviour
{
    public class BasicAttacks : MonoBehaviour
    {
        public float attackCooldown = 0.1f;
        public float timeToNextAttack = -1f;
        public float timeToCombo = 0.4f;
        public float comboTimeRemaining = -1f;
        public int attackTurn = 0;
        public int maxCombo = 3;
        public Animator animator;
        public string attackButtonParameterName = "Fire1";
        public string attackAnimatorBoolParameterName = "isAttacking";
        public string attackTurnAnimatorParameterName = "attackTurn";

        public void attack()
        {        
            if (timeToNextAttack < Time.time)
                attackAnimation();
            //else animator.SetBool(attackAnimatorBoolParameterName, false);
        }

        void attackAnimation()
        {
            animator.SetBool(attackAnimatorBoolParameterName, true);
            animator.SetInteger(attackTurnAnimatorParameterName, attackTurn);
            switch (attackTurn)
            {
                case 0:
                    timeToNextAttack = attackCooldown + Time.time;
                    comboTimeRemaining = timeToCombo + Time.time;
                    attackTurn++;
                    break;
                case 1:
                    timeToNextAttack = attackCooldown + Time.time;
                    comboTimeRemaining = timeToCombo + Time.time;
                    attackTurn++;
                    break;
                case 2:
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
    }
}
