using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskHandler : MonoBehaviour
{
    public StatManager StatsManager;
    public TaskManager taskManager;
    public DayNightController dayNightController;
    public RectTransform doingTaskPlane;
    public Text message;
    public Player player;
    public RectTransform DialoguePlane;
    public Text Dialogue;
    private string[] Tasks = {"Go to work - 8 hrs","Pharmacy run - 15 mins", "Visit grandma - 2 hrs", "Doctor visit - 1 hr", 
                                "Grocery shipping - 20 mins", "Get a Haircut - 40 mins", "Car shopping - 1 hr",
                                "Cloth shipping - 2hrs", "Find your lost dog","Work from home - 4 hrs"};
    private int day = 0;

    public MenuHandler menuHandler;
    public BoxCollider dogActionCollider;
    public BoxCollider dogTaskCollider;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int tmp = dayNightController.getNumDays();
        if(day != tmp){
            day = tmp;
            if(day == 2 || day == 17 || day == 29){
                taskManager.setTasks(Tasks[9]);//work from home
                taskManager.setTasks(Tasks[3]);//doctor
            }
            else if(day == 3 || day == 9 || day ==18 || day == 25 || day == 29){
                taskManager.setTasks(Tasks[9]);//work from home
                taskManager.setTasks(Tasks[1]);//pharmacy
            }
            else if(day == 6){
                taskManager.setTasks(Tasks[6]);//find dog
            }
            else if(day == 7 || day == 27){
                taskManager.setTasks(Tasks[4]);//grocery
            }
            else if(day == 13){
                taskManager.setTasks(Tasks[5]);//haircut
            }
            else if(day == 14 || day == 28){
                taskManager.setTasks(Tasks[2]);//grandma
            }
            else if(day ==20){
                taskManager.setTasks(Tasks[7]);//clothes shopping
            }
            else if(day == 21){
                taskManager.setTasks(Tasks[8]);//dog
            }
            else{
                taskManager.setTasks(Tasks[0]);//go to work
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit other){
        if(other.transform.CompareTag("work")){
            doTasks( "Let's work!", 0, 480f);
            //Dialogue.text = "I could really go for some coffee right about now...";
        }
        else if(other.transform.CompareTag("Pharmacy")){
            doTasks( "Oh man, do I really have to eat all those medicine？", 1, 15f);
        }
        else if(other.transform.CompareTag("Grandma")){
            doTasks("When is the last time I visit grandma... \nI should come visit her more often!!!", 2, 120f);
        }
        else if(other.transform.CompareTag("Hospital")){
            doTasks("I wonder if there is someone can help me stay health... \nI should check the community center later!", 3, 60f);
        }
        else if(other.transform.CompareTag("Dog")){
            doTasks( "Oh, that's where you were buddy!",8,0.0f);
        }
        else if(other.transform.CompareTag("Haircut")){
            doTasks( "Oh no my face is itchy. \nShould I itch it? I don't know how to get my hands out from under the cape!",5,40f);
        }
        else if(other.transform.CompareTag("Car")){
            doTasks( "OMG, look all those cars!!!",6,60f);
        }
        else if(other.transform.CompareTag("Cloth")){
            doTasks( "Will I look good in this color?",7,120f);
        }
        else if(other.transform.CompareTag("Grocery")){
            doTasks( "I wish eating healthy could be more affordable, this is gonna be tough to keep up",4,20f);
        }
        else if(other.transform.CompareTag("WorkFhome")){
            doTasks( "Ugh, work work work...",9,240f);
        }
    }

    public void doTasks(string m, int task,float time){
        if(taskManager.hasTask(Tasks[task]) == true){
            //doingTaskPlane.gameObject.SetActive(true);
            DialoguePlane.gameObject.SetActive(true);
            //message.text = m;
            Dialogue.text = m;
            StartCoroutine(Wait());
            taskManager.removeTask(Tasks[task]);
            menuHandler.hideMenu();
            player.playerSkipTime(time);

            // If dog task, then we disable some colliders
            if(task == 8)
            {
                dogActionCollider.enabled = true;
                dogTaskCollider.enabled = false;
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        //doingTaskPlane.gameObject.SetActive(false);
        DialoguePlane.gameObject.SetActive(false);
        Dialogue.text = "";
    }
}
