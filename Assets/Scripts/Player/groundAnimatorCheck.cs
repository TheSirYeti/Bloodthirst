using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Animations
{
    public class groundAnimatorCheck : MonoBehaviour
    {
        public Animator animator;
        public PlayerController player;
        public string groundParameterName = "isOnGround";
        public string landingParameterName = "isLanding";
        private int currentColliderStatus;
        private float landingAnimationCooldown = 0.2f;
        private float resetLandingAnimation;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Floor" || other.gameObject.tag == "SpinnyThing")
            {
                currentColliderStatus++;
                if (currentColliderStatus > 0)
                {
                    animator.SetBool(landingParameterName, true);
                    resetLandingAnimation = Time.time + landingAnimationCooldown;
                }

                if (other.gameObject.tag == "SpinnyThing")
                {
                    player.transform.parent = other.transform;
                }
            }
        }

      

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == "Floor" || other.gameObject.tag == "SpinnyThing")
            {
                currentColliderStatus--;
                if (currentColliderStatus < 1)
                {
                    animator.SetBool(groundParameterName, false);
                    animator.SetBool(landingParameterName, false);
                }

                if(other.gameObject.tag == "SpinnyThing")
                {
                    player.transform.parent = null;
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Floor" || other.gameObject.tag == "SpinnyThing")
            {
                if (resetLandingAnimation <= Time.time)
                {
                    resetLandingAnimation = Time.time + landingAnimationCooldown;
                    animator.SetBool(landingParameterName, true);
                    animator.SetBool(groundParameterName, true);
                }

                if (other.gameObject.tag == "SpinnyThing")
                {
                    player.transform.parent = other.transform;
                }
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
