using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    public Transform camera;
    public Transform icon;
    public Transform mapIcons;

    private Camera minimapCamera;
    

    private float currentZoom;
    private float minZoom = 5;
    private float maxZoom = 50;
    private float increments = 1;

    void Start()
    {
        minimapCamera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadMinus) && minimapCamera.orthographicSize < maxZoom)
        {
            minimapCamera.orthographicSize = minimapCamera.orthographicSize + increments;
            icon.transform.localScale = new Vector3(minimapCamera.orthographicSize, minimapCamera.orthographicSize, minimapCamera.orthographicSize);
            foreach (Transform child in mapIcons)
            {
                child.transform.localScale = new Vector3(minimapCamera.orthographicSize, minimapCamera.orthographicSize, minimapCamera.orthographicSize);
            }
        }    
        if (Input.GetKey(KeyCode.KeypadPlus) && minimapCamera.orthographicSize > minZoom)
        {
            minimapCamera.orthographicSize = minimapCamera.orthographicSize - increments;
            icon.transform.localScale = new Vector3(minimapCamera.orthographicSize, minimapCamera.orthographicSize, minimapCamera.orthographicSize);
            foreach (Transform child in mapIcons)
            {
                child.transform.localScale = new Vector3(minimapCamera.orthographicSize, minimapCamera.orthographicSize, minimapCamera.orthographicSize);
            }
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
