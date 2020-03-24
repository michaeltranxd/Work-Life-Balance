using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public float mouseSensitivity = 50f;

    public Transform playerBody;
    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraOffset = transform.position - playerBody.transform.position;
    }

    // LateUpdate called after Update methods
    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        Quaternion camTurnAngle = Quaternion.AngleAxis(mouseX, Vector3.up);
        cameraOffset = camTurnAngle * cameraOffset;

        transform.position = playerBody.transform.position + cameraOffset;
        transform.LookAt(playerBody);
    }

}
