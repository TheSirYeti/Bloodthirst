using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFX.Player
{
    public class PlayerVFX : MonoBehaviour
    {
        public List<ParticleSystem> vfx = new List<ParticleSystem>();

        private float disableTimer = 0.5f;
        private float cooldown;

        private void Start()
        {
            foreach (ParticleSystem particleSystem in vfx)
            {
                particleSystem.Stop();
            }
        }

        private void FixedUpdate()
        {
            if(cooldown < Time.time)
            {
                foreach (ParticleSystem particleSystem in vfx)
                {
                    particleSystem.Stop();
                }
            }
        }

        public void enableVFX(int index)
        {
            vfx[index].Play();
            cooldown = disableTimer + Time.time;
        }

        public void disableVFX(int index)
        {
            vfx[index].Stop();
        }
    }
}
