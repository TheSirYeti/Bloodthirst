using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX.Player
{
    public class PlayerVFX : MonoBehaviour
    {
        public List<ParticleSystem> dualGroundAttackVFX = new List<ParticleSystem>();
        public List<ParticleSystem> heavyGroundAttackVFX = new List<ParticleSystem>();
        public List<ParticleSystem> heavyGroundExplosionVFX = new List<ParticleSystem>();
        public List<ParticleSystem> airAttackVFX = new List<ParticleSystem>();


        private float disableTimer = 0.5f;
        private float cooldown;

        private void Awake()
        {
            foreach (ParticleSystem particleSystem in dualGroundAttackVFX)
            {
                particleSystem.Stop();
            }

            foreach (ParticleSystem particleSystem in airAttackVFX)
            {
                particleSystem.Stop();
            }

            foreach (ParticleSystem particleSystem in heavyGroundAttackVFX)
            {
                particleSystem.Stop();
            }
        }

        private void FixedUpdate()
        {
            if(cooldown < Time.time)
            {
                foreach (ParticleSystem particleSystem in dualGroundAttackVFX)
                {
                    particleSystem.Stop();
                }
                foreach (ParticleSystem particleSystem in airAttackVFX)
                {
                    particleSystem.Stop();
                }
                foreach (ParticleSystem particleSystem in heavyGroundAttackVFX)
                {
                    particleSystem.Stop();
                }
                foreach (ParticleSystem particleSystem in heavyGroundExplosionVFX)
                {
                    particleSystem.Stop();
                }
            }
        }

        public void enableDualGroundVFX(int index)
        {
            dualGroundAttackVFX[index].Play();
            cooldown = disableTimer + Time.time;
        }

        public void disableDualGroundVFX(int index)
        {
            dualGroundAttackVFX[index].Stop();
        }

        public void enableHeavyGroundVFX(int index, float time)
        {
            StartCoroutine(toggleHeavyVFX(index, time));
        }

        IEnumerator toggleHeavyVFX(int index, float time)
        {
            yield return new WaitForSeconds(time);
            //heavyGroundAttackVFX[index].Play();
            yield return new WaitForSeconds(0.5f);
            heavyGroundExplosionVFX[0].Play();
            heavyGroundExplosionVFX[1].Play();
            heavyGroundExplosionVFX[2].Play();
            SoundManager.instance.Play(SoundID.EXPLOSION1, false, 0.2f, 1);
            cooldown = disableTimer + Time.time;
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
