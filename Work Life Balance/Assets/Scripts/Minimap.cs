using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    public Transform camera;

    private Camera minimapCamera;

    private float currentZoom;
    private float minZoom = 5;
    private float maxZoom = 50;
    private float increments = 2;

    void Start()
    {
        minimapCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus) && minimapCamera.orthographicSize < maxZoom)
        {
            minimapCamera.orthographicSize = minimapCamera.orthographicSize + increments;
        }    
        if (Input.GetKeyDown(KeyCode.KeypadPlus) && minimapCamera.orthographicSize > minZoom)
        {
            minimapCamera.orthographicSize = minimapCamera.orthographicSize - increments;
        }
    }
    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, camera.eulerAngles.y, 0);
    }
}
