using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Animations
{
    public class groundAnimatorCheck : MonoBehaviour
    {
        public Animator animator;
        public string groundParameterName = "isOnGround";
        public string landingParameterName = "isLanding";
        private int currentColliderStatus;
        private float landingAnimationCooldown = 0.2f;
        private float resetLandingAnimation;

        private void OnTriggerEnter(Collider other)
        {
            currentColliderStatus++;
            if (currentColliderStatus > 0)
            {
                animator.SetBool(landingParameterName, true);
                resetLandingAnimation = Time.time + landingAnimationCooldown;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            currentColliderStatus--;
            if (currentColliderStatus < 1)
            {
                animator.SetBool(groundParameterName, false);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (resetLandingAnimation <= Time.time)
            {
                animator.SetBool(landingParameterName, false);
                animator.SetBool(groundParameterName, true);
            }
        }
        public int getStatus()
        {
            return currentColliderStatus;
        }

        public bool getLandingStatus()
        {
            return animator.GetBool(landingParameterName);
        }

    }
}
