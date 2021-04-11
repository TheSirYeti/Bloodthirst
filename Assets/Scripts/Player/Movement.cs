using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public      string horizontalAxis = "Horizontal";
    public      string verticalAxis = "Vertical";
    public      string jumpButtonName = "Jump";
    public      float movementSpeed = 2f;
    public      float jumpForce = 10f;
    public      ForceMode jumpForceMode = ForceMode.Impulse;
    public      Rigidbody rigidBody;
    private     Vector3 inputVector;

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
        //myRigidBody.MoveRotation(transform.rotation * Quaternion.Euler(airplaneRotationSpeed * airplaneRotationInput * Time.deltaTime, rotationSpeed * rotationInput * Time.deltaTime, 0));
        transform.LookAt(transform.position + inputVector);

        rigidBody.MovePosition(transform.position + inputVector * (movementSpeed * Time.deltaTime));
    }
}
