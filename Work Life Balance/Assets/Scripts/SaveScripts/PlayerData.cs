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

        Vector3 cameraOriginalPosition = CameraFollowPlayer.originalPosition;

        cameraPosition = new float[3];
        cameraPosition[0] = cameraOriginalPosition.x;
        cameraPosition[1] = cameraOriginalPosition.y;
        cameraPosition[2] = cameraOriginalPosition.z;

        time = dayNightController.getCurrentTimeOfDay();
        day = dayNightController.getNumDays();
    }
}
