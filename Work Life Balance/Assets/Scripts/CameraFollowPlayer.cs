using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public float mouseSensitivity = 50f;

    public Player playerCharacter;
    public Transform cameraFollow;
    private Vector3 cameraOffset;
    private Vector3 maxCameraOffset;
    private float maxCameraMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        maxCameraOffset = transform.position - cameraFollow.transform.position;
        maxCameraMagnitude = maxCameraOffset.magnitude;
        cameraOffset = maxCameraOffset;
    }

    // LateUpdate called after Update methods
    void LateUpdate()
    {
        if (playerCharacter.playerInControl)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            Quaternion camTurnAngle = Quaternion.AngleAxis(mouseX, Vector3.up);
            cameraOffset = camTurnAngle * cameraOffset;
            

            RaycastHit raycast;
            bool hit = Physics.Raycast(playerCharacter.transform.position, cameraOffset, out raycast, cameraOffset.magnitude);

            Debug.DrawLine(playerCharacter.transform.position, transform.position, Color.red);

            if (hit)
            {
                Vector3 scaleCamera = cameraOffset / (1.01f);
                if((cameraFollow.transform.position - raycast.point).sqrMagnitude - 1.2f < scaleCamera.sqrMagnitude)
                {
                    cameraOffset = scaleCamera;
                }
                print("cam" + scaleCamera.magnitude);
                print("player" + (cameraFollow.transform.position - raycast.point).magnitude);
            }
            else
            {
                Vector3 scaleCamera = cameraOffset * (1.01f);
                if (scaleCamera.magnitude < maxCameraMagnitude)
                {
                    cameraOffset = scaleCamera;
                }

            }

            transform.position = cameraFollow.transform.position + cameraOffset;
            transform.LookAt(cameraFollow);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, cameraOffset);
    }

}
