using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKeys : MonoBehaviour
{
    public RectTransform controlPanel;
    private bool keyActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //show control screen
        if(keyActive == true && Input.GetKeyDown(KeyCode.H)){
            controlPanel.gameObject.SetActive(false);
            keyActive = false;
        }
        //close controk screen
        else if(keyActive == false && Input.GetKeyDown(KeyCode.H)){
            controlPanel.gameObject.SetActive(true);
            keyActive = true;
        } 
    }
}
