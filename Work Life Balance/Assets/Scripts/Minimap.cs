using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform characterTransform;
    public Transform playerCamera;
    public Transform minimapIcon;
    public Transform minimapIcons;

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
            minimapIcon.transform.localScale = minimapIcon.transform.localScale + Vector3.one;
            foreach (Transform child in minimapIcons)
            {
                child.transform.localScale = child.transform.localScale + Vector3.one;
            }
        }    
        if (Input.GetKey(KeyCode.KeypadPlus) && minimapCamera.orthographicSize > minZoom)
        {
            minimapCamera.orthographicSize = minimapCamera.orthographicSize - increments;
            minimapIcon.transform.localScale = minimapIcon.transform.localScale - Vector3.one;
            foreach (Transform child in minimapIcons)
            {
                child.transform.localScale = child.transform.localScale - Vector3.one;
            }
        }
    }
    void LateUpdate()
    {
        Vector3 newPosition = characterTransform.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, playerCamera.eulerAngles.y, 0);
    }
}
