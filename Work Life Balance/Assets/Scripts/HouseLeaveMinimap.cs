using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseLeaveMinimap : MonoBehaviour
{
    public Transform MinimapIcons;

    void OnTriggerEnter(Collider other)
    {
        if(other && other.tag == "Player")
        {
            foreach(Transform child in MinimapIcons)
            {
                if (child.name.Contains("InHouse") && child.gameObject.activeSelf){
                    child.gameObject.SetActive(false);
                }
                if (child.name.Equals("HouseIcon"))
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
