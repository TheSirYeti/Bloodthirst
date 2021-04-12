using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public      string              horizontalAxis              = "Horizontal";
    public      string              verticalAxis                = "Vertical";
    public      string              jumpButtonName              = "Jump";
    public      float               movementSpeed               = 2f;
    public      float               jumpForce                   = 10f;
    public      ForceMode           jumpForceMode               = ForceMode.Impulse;
    public      Rigidbody           rigidBody;
    public      int                 jumpCount;
    public      int                 maxJumpCount                = 2;
    public      Animator            animator;
    public      string              runningSpeedParameterName   = "runningSpeed";
    public      Transform           cameraDirection;
    private     Vector3             inputVector;

    void Update()
    {
        inputVector.x = Input.GetAxis(horizontalAxis);

        inputVector.z = Input.GetAxis(verticalAxis);

        if (Input.GetButtonDown(jumpButtonName))
        {
            rigidBody.AddForce(Vector3.up * jumpForce, jumpForceMode);
        }
    }

    private void FixedUpdate()
    {
        transform.LookAt(transform.position + inputVector);

        rigidBody.MovePosition(transform.position + inputVector * (movementSpeed * Time.deltaTime));
        animator.SetFloat(runningSpeedParameterName, inputVector.magnitude);
    }
}
