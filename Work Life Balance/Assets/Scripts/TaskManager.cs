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
            ToDoList = ToDoList + task + "\n";
            AllTasks.Add(task);
            Debug.Log("Adding Task " + ToDoList);
        }
    }

    public void removeTask(string task){
        AllTasks.Remove(task);
        ToDoList = ToDoList.Replace(task,string.Empty);
    }
    public void checkTasks(){

        if(AllTasks.Count > 0){
            StatsManager.AddAbility(-(AllTasks.Count* 10.0f));
            Debug.Log("AddAbility: " + (-(AllTasks.Count* 10.0f)));
        }
        AllTasks.Remove("Go to work");
        AllTasks.Remove("Work from home");
    }

    public bool hasTask(string task){
        return AllTasks.Contains(task);
    }
}
