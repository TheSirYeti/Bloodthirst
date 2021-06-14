using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Animations;

namespace Player.Behaviour
{
    public class Movement : MonoBehaviour
    {
        public      string                      horizontalAxis              = "Horizontal";
        public      string                      verticalAxis                = "Vertical";
        public      string                      jumpButtonName              = "Jump";
        public      float                       movementSpeed               = 2f;
        public      float                       jumpForce                   = 10f;
        public      ForceMode                   jumpForceMode               = ForceMode.Impulse;
        public      Rigidbody                   rigidBody;
        public      int                         jumpCount;
        public      int                         maxJumpCount                = 1;
        public      Animator                    animator;
        public      string                      runningSpeedParameterName   = "runningSpeed";
        public      Transform                   cameraDirection;
        public      groundAnimatorCheck         groundCheck;
        public      BasicAttacks                basicAttacks;
        private     Vector3                     inputVector                 = Vector3.zero;
        private     float                       originalMovementSpeedValue;
        public      bool                        TwoDimensionMovement;
        public      bool                        runningSFXPlaying;

        private     float                       floatingTime = 0.75f;
        private     float                       cooldown;
        private     float                       rollCooldown;
        public      bool                        isAttacking;
        

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


            if(isAttacking == false)
            {
                movementSpeed = originalMovementSpeedValue;
            } else
            {
                movementSpeed = 0.65f;
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

            if (!isAttacking)
            {
                transform.LookAt(transform.position + inputVector);
            }
            transform.position += inputVector * (movementSpeed * Time.deltaTime);

            if ((xValue == 0 && Input.GetAxis(horizontalAxis) == 0) || !animator.GetBool("isOnGround") || movementSpeed == 0f)
            {
                runningSFXPlaying = false;
            } else if (!runningSFXPlaying && animator.GetBool("isOnGround"))
            {
                runningSFXPlaying = true;
            }

            animator.SetFloat(runningSpeedParameterName, inputVector.magnitude);
            
        }

        public void Jump()
        {
            if (!isAttacking)
            {
                if (animator.GetBool("isOnGround") || jumpCount < maxJumpCount - 1)
                {
                    rigidBody.velocity = new Vector3(0f, 0f, 0f);
                    rigidBody.AddForce(Vector3.up * jumpForce, jumpForceMode);
                    //SoundManager.instance.PlaySound(SoundID.JUMP, false, 1);
                    jumpCount++;
                }

                if (groundCheck.getStatus() > 0)
                    jumpCount = 0;
            }
        }

        public void restrictMovement(float time)
        {
            if (groundCheck.getLandingStatus())
            {
                StartCoroutine(stopMovement(time));
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

        IEnumerator stopMovement(float time)
        {
            isAttacking = true;
            yield return new WaitForSeconds(time);
            isAttacking = false;
        }

        public IEnumerator stopRotation()
        {
            isAttacking = true;
            yield return new WaitForSeconds(0.75f);
            isAttacking = false;
        }

        IEnumerator lowerSpeed()
        {
            movementSpeed = 0.4f;
            yield return new WaitForSeconds(0.45f);
            movementSpeed = 2f;
        }

        public void roll()
        {
            float horizontalInput = Input.GetAxis(horizontalAxis);
            float verticalInput = Input.GetAxis(verticalAxis);
            if (rollCooldown <= Time.time && checkInput(horizontalInput, verticalInput))
            {
                Vector3 rollDirection = new Vector3(horizontalInput, 0f, verticalInput);
                animator.SetTrigger("roll");
                rigidBody.AddForce(rollDirection * 100, ForceMode.Impulse);
                rollCooldown = Time.time + 1f;
            }
        }

        bool checkInput(float hInput, float vInput)
        {
            if (hInput >= 0.5f || hInput <= -0.5f || vInput >= 0.5f || vInput <= -0.5f)
                return true;
            else return false;
        }
    }
}
