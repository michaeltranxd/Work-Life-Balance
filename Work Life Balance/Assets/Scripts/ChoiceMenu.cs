using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceMenu : MonoBehaviour
{
    void Start()
    {
        Debug.Log("ChoiceMenu started");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnButtonClick()
    {
        //Debug.Log(this.gameObject.name + " has been clicked");
    }

}
