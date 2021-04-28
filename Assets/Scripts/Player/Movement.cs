using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Animations;

namespace Player.Behaviour
{
    public class Movement : MonoBehaviour
    {
        public      string                  horizontalAxis              = "Horizontal";
        public      string                  verticalAxis                = "Vertical";
        public      string                  jumpButtonName              = "Jump";
        public      float                   movementSpeed               = 2f;
        public      float                   jumpForce                   = 10f;
        public      ForceMode               jumpForceMode               = ForceMode.Impulse;
        public      Rigidbody               rigidBody;
        public      int                     jumpCount;
        public      int                     maxJumpCount                = 1;
        public      Animator                animator;
        public      string                  runningSpeedParameterName   = "runningSpeed";
        public      Transform               cameraDirection;
        public      groundAnimatorCheck     groundCheck;
        public      BasicAttacks            basicAttacks;
        private     Vector3                 inputVector                 = Vector3.zero;
        private     float                   originalMovementSpeedValue;
        public      bool                    TwoDimensionMovement;

        private float floatingTime = 0.75f;
        private float cooldown;


        private void Start()
        {
            TwoDimensionMovement = false;
            originalMovementSpeedValue = movementSpeed;
        }

        private void FixedUpdate()
        {
            if(cooldown < Time.time)
            {
                rigidBody.useGravity = true;
            }
        }

        public void Move()
        {
            float xValue;
            if (TwoDimensionMovement)
            {
                xValue = 0;
                if(IsBetween(transform.rotation.y, 0f, 90f))
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                } else transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            }
            else xValue = Input.GetAxis(verticalAxis);
            inputVector = cameraDirection.forward * xValue + cameraDirection.right * Input.GetAxis(horizontalAxis);
            inputVector.y = 0f;

            transform.LookAt(transform.position + inputVector);

                rigidBody.MovePosition(transform.position + inputVector * (movementSpeed * Time.deltaTime));
                animator.SetFloat(runningSpeedParameterName, inputVector.magnitude);
            
        }

        public void Jump()
        {
            if (!basicAttacks.isAttacking())
            {
                if (animator.GetBool("isOnGround") || jumpCount < maxJumpCount - 1)
                {
                    rigidBody.velocity = new Vector3(0f, 0f, 0f);
                    rigidBody.AddForce(Vector3.up * jumpForce, jumpForceMode);
                    jumpCount++;
                }

                if (groundCheck.getStatus() > 0)
                    jumpCount = 0;
            }
        }

        public void restrictMovement()
        {
            if (groundCheck.getLandingStatus())
            {
                movementSpeed = 0f;
            }
            else movementSpeed = originalMovementSpeedValue;
        }

        public void toggleFloat()
        {
            rigidBody.useGravity = false;
            cooldown = floatingTime + Time.time;
        }

        public bool IsBetween(float value, float bound1, float bound2)
        {
            return (value >= Mathf.Min(bound1, bound2) && value <= Mathf.Max(bound1, bound2));
        }
    }
}
