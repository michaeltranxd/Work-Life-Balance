using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public StatManager StatsManager;
    public RectTransform TaskPanel;
    public Text ListofTask;
    private string ToDoList;
    private string[] Tasks = {"Go to work - 8 hrs","Pharmacy run - 15 mins", "Visit grandma - 2 hrs", "Doctor visit - 1 hr", 
                                "Grocery shipping - 20 mins", "Get a Haircut - 40 mins", "Car shopping - 1 hr",
                                "Cloth shipping - 2hrs", "Find your lost dog","Work from home - 4 hrs"};
    private List<string> AllTasks = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            TaskPanel.gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            TaskPanel.gameObject.SetActive(false);
        }
        ListofTask.text = ToDoList;
    }

    public void setTasks(string task){
        if(AllTasks.Contains(task) == false){
            //ToDoList = ToDoList + task + "\n";
            AllTasks.Add(task);
            Debug.Log("Adding Task " + ToDoList);
        }
        ToDoList = string.Join( "\n", AllTasks.ToArray() );
        print("set: " + ToDoList);
    }

    public void removeTask(string task){
        AllTasks.Remove(task);
        ToDoList = string.Join( "\n", AllTasks.ToArray() );
        print("remove: " +ToDoList);
        //ToDoList = ToDoList.Replace(task,string.Empty);
    }
    public void checkTasks(){

        if(AllTasks.Count > 0){
            StatsManager.AddAbility(-(AllTasks.Count* 10.0f));
            if(hasTask(Tasks[0] ) == true){ //go to work
                StatsManager.AddMent(-10.0f);
            }
            if(hasTask(Tasks[1]) == true){// pharmacy
                StatsManager.AddPhys(-5.0f);
            }
            if(hasTask(Tasks[2]) == true){//grandma
                StatsManager.AddMent(-3.0f);
            }
            if(hasTask(Tasks[3]) == true){//doctor
                StatsManager.AddPhys(-10.0f);
            }
            if(hasTask(Tasks[4]) == true){//grocery
                StatsManager.AddPhys(-10.0f);
            }
            if(hasTask(Tasks[5]) == true){//haircut
                StatsManager.AddNutri(-5.0f);
            }
            if(hasTask(Tasks[6]) == true){//car
                StatsManager.AddMent(-2.0f);
            }
            if(hasTask(Tasks[7]) == true){//clothes
                StatsManager.AddMent(-2.0f);
            }
            if(hasTask(Tasks[8]) == true){//dog
                StatsManager.AddMent(-10.0f);
            }
            if(hasTask(Tasks[9]) == true){//work from home
                StatsManager.AddMent(-5.0f);
            }
        }

        removeTask("Go to work");
        removeTask("Work from home");
    }

    public bool hasTask(string task){
        return AllTasks.Contains(task);
    }
}
