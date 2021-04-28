using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirection : MonoBehaviour
{
    public GameObject Character;
    public GameObject CameraCenter;
    public float yOffset;
    public float sensitivity;
    public Camera cam;
    RaycastHit camHit;
    public Vector3 CamDist;
    void Start()
    {
        CamDist = cam.transform.localPosition;
        Cursor.visible = false;
    }

    void Update()
    {
        CameraCenter.transform.position = new Vector3(Character.transform.position.x, Character.transform.position.y + yOffset, Character.transform.position.z);
        CameraCenter.transform.rotation = Quaternion.Euler(CameraCenter.transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * sensitivity / 2,
            CameraCenter.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivity, CameraCenter.transform.rotation.eulerAngles.z);

        cam.transform.localPosition = CamDist;
        GameObject obj = new GameObject();
        obj.transform.SetParent(cam.transform.parent);
        obj.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, cam.transform.localPosition.z - 0.1f);
        if (Physics.Linecast(CameraCenter.transform.position, obj.transform.position, out camHit))
        {
            cam.transform.position = camHit.point;
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, cam.transform.localPosition.z + 0.1f);
        }
        Destroy(obj);
    }
}
