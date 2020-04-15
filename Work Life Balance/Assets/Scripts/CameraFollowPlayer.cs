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

    RaycastHit raycast;
    bool hit;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        maxCameraOffset = transform.position - cameraFollow.transform.position;
        maxCameraMagnitude = maxCameraOffset.magnitude;
        cameraOffset = maxCameraOffset;
    }

    void Update()
    {
        hit = Physics.Raycast(playerCharacter.transform.position, cameraOffset, out raycast, cameraOffset.magnitude);
    }

    // LateUpdate called after Update methods
    void LateUpdate()
    {
        if (DayNightController.GameWon || StatManager.GameOver)
            return;
        if (playerCharacter.playerInControl)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            Quaternion camTurnAngle = Quaternion.AngleAxis(mouseX, Vector3.up);

            if (!Player.cursorShown)
                cameraOffset = camTurnAngle * cameraOffset;            

            Debug.DrawLine(playerCharacter.transform.position, transform.position, Color.red);

            if (hit)
            {
                if (!raycast.transform.tag.Equals("Player"))
                {
                    Vector3 scaleCamera = cameraOffset / (1.05f);
                    if ((cameraFollow.transform.position - raycast.point).sqrMagnitude - 1.5f < scaleCamera.sqrMagnitude)
                    {
                        cameraOffset = scaleCamera;
                    }
                    print("cam" + scaleCamera.magnitude);
                    print("player" + (cameraFollow.transform.position - raycast.point).magnitude);
                }

            }
            else
            {
                Vector3 scaleCamera = cameraOffset * (1.05f);
                if (scaleCamera.magnitude < maxCameraMagnitude)
                {
                    cameraOffset = scaleCamera;
                }

            }

            transform.position = cameraFollow.transform.position + cameraOffset;
            transform.LookAt(cameraFollow);
        }
    }

}
