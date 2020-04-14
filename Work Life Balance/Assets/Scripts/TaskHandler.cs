using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskHandler : MonoBehaviour
{
    public StatManager StatsManager;
    public TaskManager taskManager;
    private string[] Tasks = {"Pharmacy run", "Visit grandma", "Doctor visit"};
    // Start is called before the first frame update
    void Start()
    {
        taskManager.setTasks(Tasks[0]);
        taskManager.setTasks(Tasks[1]);
        taskManager.setTasks(Tasks[2]);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnControllerColliderHit(ControllerColliderHit other){
        if(other.transform.CompareTag("Pharmacy")){
            taskManager.removeTask(Tasks[0]);
        }
        else if(other.transform.CompareTag("Apartments")){
            taskManager.removeTask(Tasks[1]);
        }
        else if(other.transform.CompareTag("Hospital")){
            taskManager.removeTask(Tasks[2]);
        }
    }
}
