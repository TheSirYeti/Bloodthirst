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

        public List<GameObject> bullets = new List<GameObject>();

        public GameObject specialMeter;

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

            foreach (GameObject g in bullets)
            {
                g.SetActive(false);
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
                    //particleSystem.gameObject.transform.parent = transform;
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

        public void enableHeavyGroundVFX(int index)
        {
            heavyGroundAttackVFX[index].Play();
            //heavyGroundAttackVFX[index].gameObject.transform.parent = null;
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

        public void vfxBullet(int index)
        {
            GameObject bullet = Instantiate(bullets[index], transform);
            bullet.SetActive(true);
            bullet.GetComponent<BulletBehaviour>().fire = true;
        }

        public void enableSparks()
        {
            specialMeter.SetActive(true);
        }

        public void disableSparks()
        {
            specialMeter.SetActive(false);
        }
    }
}
