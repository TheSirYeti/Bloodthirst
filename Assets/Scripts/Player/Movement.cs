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


        private void Start()
        {
            originalMovementSpeedValue = movementSpeed;
        }

        public void Move()
        {
            inputVector = cameraDirection.forward * Input.GetAxis(verticalAxis) + cameraDirection.right * Input.GetAxis(horizontalAxis);
            inputVector.y = 0f;

            transform.LookAt(transform.position + inputVector);
            if (!basicAttacks.isAttacking())
            {
                rigidBody.MovePosition(transform.position + inputVector * (movementSpeed * Time.deltaTime));
                animator.SetFloat(runningSpeedParameterName, inputVector.magnitude);
            }
        }

        public void Jump()
        {
            if (animator.GetBool("isOnGround") || jumpCount < maxJumpCount - 1)
            {
                rigidBody.AddForce(Vector3.up * jumpForce, jumpForceMode);
                jumpCount++;
            }

            if (groundCheck.getStatus() > 0)
                jumpCount = 0;
        }

        public void restrictMovement()
        {
            if (groundCheck.getLandingStatus())
            {
                movementSpeed = 0f;
            }
            else movementSpeed = originalMovementSpeedValue;
        }
    }
}
