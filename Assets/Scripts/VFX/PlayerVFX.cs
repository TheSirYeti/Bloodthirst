using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX.Player
{
    public class PlayerVFX : MonoBehaviour
    {
        public List<ParticleSystem> groundAttackVFX = new List<ParticleSystem>();
        public List<ParticleSystem> airAttackVFX = new List<ParticleSystem>();


        private float disableTimer = 0.5f;
        private float cooldown;

        private void Start()
        {
            foreach (ParticleSystem particleSystem in groundAttackVFX)
            {
                particleSystem.Stop();
            }

            foreach (ParticleSystem particleSystem in airAttackVFX)
            {
                particleSystem.Stop();
            }
        }

        private void FixedUpdate()
        {
            if(cooldown < Time.time)
            {
                foreach (ParticleSystem particleSystem in groundAttackVFX)
                {
                    particleSystem.Stop();
                }
            }
        }

        public void enableGroundVFX(int index)
        {
            groundAttackVFX[index].Play();
            cooldown = disableTimer + Time.time;
        }

        public void disableGroundVFX(int index)
        {
            groundAttackVFX[index].Stop();
        }

        public void enableAirVFX(int index)
        {
            airAttackVFX[index].Play();
            cooldown = disableTimer + Time.time;
        }

        public void disableAirVFX(int index)
        {
            airAttackVFX[index].Stop();
        }
    }
}
