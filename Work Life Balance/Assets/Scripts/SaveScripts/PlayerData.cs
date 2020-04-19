using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Able to save in file
public class PlayerData
{
    public float[] position;
    public float[] cameraPosition;
    public float[] stats;
    public float time;
    public int day;

    public PlayerData(Player player, Camera camera, StatManager statManager, DayNightController dayNightController)
    {
        stats = new float[6];
        stats[0] = statManager.GetPhys();
        stats[1] = statManager.GetMent();
        stats[2] = statManager.GetNutri();
        stats[3] = statManager.GetHygiene();
        stats[4] = statManager.GetEnergy();
        stats[5] = statManager.GetAbility();
        
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        cameraPosition = new float[3];
        cameraPosition[0] = camera.transform.position.x;
        cameraPosition[1] = camera.transform.position.y;
        cameraPosition[2] = camera.transform.position.z;

        time = dayNightController.getCurrentTimeOfDay();
        day = dayNightController.getNumDays();
    }
}
