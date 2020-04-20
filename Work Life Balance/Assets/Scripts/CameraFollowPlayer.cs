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
    private float cameraMagnitude;

    public static Vector3 originalPosition;

    RaycastHit raycast;
    bool hit;

    // Start is called before the first frame update

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        maxCameraOffset = transform.position - cameraFollow.transform.position;
        maxCameraMagnitude = 3.5f;
        cameraMagnitude = maxCameraMagnitude;
        cameraOffset = maxCameraOffset;

        if (LevelLoader.LoadingSavedFile)
            LoadCamera();
    }

    void Start()
    {

    }

    void Update()
    {
        hit = Physics.Raycast(cameraFollow.transform.position, cameraOffset, out raycast, cameraOffset.magnitude);
        Debug.DrawLine(cameraFollow.transform.position, transform.position, Color.red);
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

            if (hit)
            {
                if (!raycast.transform.tag.Equals("Player"))
                {
                    cameraMagnitude = Mathf.Clamp(raycast.distance - 0.5f, 1f, maxCameraMagnitude);
                }

            }
            else
            {
                cameraMagnitude = Mathf.Lerp(cameraMagnitude, maxCameraMagnitude, 0.05f);
            }

            transform.position = cameraFollow.transform.position + cameraOffset.normalized * cameraMagnitude;
            transform.LookAt(cameraFollow);
        }

        originalPosition = cameraFollow.transform.position + cameraOffset.normalized * maxCameraMagnitude;
    }

    public void LoadCamera()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.cameraPosition[0];
        position.y = data.cameraPosition[1];
        position.z = data.cameraPosition[2];
        transform.position = position;

    }

}
