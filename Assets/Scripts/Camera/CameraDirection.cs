using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirection : MonoBehaviour
{
    
    /*public      GameObject      cameraTarget;
    public      Vector3         distanceFromObject;
    public      float           smoothingSpeed = 0.2f;
    public      float           cameraMovementSpeed;


    void Update()
    {
        transform.LookAt(cameraTarget.transform);
        checkMouseInput();
        checkCameraPosition();
    }

    private void LateUpdate()
    {
        Vector3 newPosition = cameraTarget.transform.position + distanceFromObject;
        Vector3 smoothTransition = Vector3.Lerp(transform.position, newPosition, smoothingSpeed);
        transform.position = smoothTransition;
    }

    public void checkMouseInput()
    {
        if (Input.GetAxis("Mouse X") > 0)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * cameraMovementSpeed,
                                       0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * cameraMovementSpeed);
        }

        else if (Input.GetAxis("Mouse X") < 0)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * cameraMovementSpeed,
                                       0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * cameraMovementSpeed);
        }
    }

    public void checkCameraPosition()
    {

    }*/
}
