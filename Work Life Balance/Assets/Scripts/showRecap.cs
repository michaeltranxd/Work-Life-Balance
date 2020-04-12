using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showRecap : MonoBehaviour
{

    public RectTransform recapPlane;
    public Recap recap;


    private void OnTriggerEnter(Collider other){
        if(other.tag.Equals("Recap")){
            recapPlane.gameObject.SetActive(true);
            recap.startTyping();
        }
    }
}
