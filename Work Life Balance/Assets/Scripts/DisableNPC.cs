using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableNPC : MonoBehaviour
{
    public GameObject NPCs;
    public DayNightController dayNightController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dayNightController.getCurrentHour() == 19 && dayNightController.getCurrentMinute() == 0){
            NPCs.SetActive(false);
        }
        else if(dayNightController.getCurrentHour() == 9 && dayNightController.getCurrentMinute() == 0){
            NPCs.SetActive(true);
        }
    }
}
