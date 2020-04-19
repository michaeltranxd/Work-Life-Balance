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
    private string[] Tasks = {"Go to work","Pharmacy run", "Visit grandma", "Doctor visit", 
                                "Grocery shipping(Supermarket)", "Get a Haircut", "Car shipping",
                                "Cloth shipping", "Buy fresh produce","Work from home"};
    private int day = 0;

    public MenuHandler menuHandler;
    
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
                taskManager.setTasks(Tasks[4]);//supermarket
            }
            else if(day == 13){
                taskManager.setTasks(Tasks[5]);//haircut
            }
            else if(day == 14 || day == 28){
                taskManager.setTasks(Tasks[2]);//grandma
            }
            else if(day ==20){
                taskManager.setTasks(Tasks[7]);//cloth shopping
            }
            else if(day == 21){
                taskManager.setTasks(Tasks[8]);//fresh produce
            }
            else{
                taskManager.setTasks(Tasks[0]);//go to work
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit other){
        if(other.transform.CompareTag("work")){
            doTasks( "Working for 6 hours...",0);
        }
        else if(other.transform.CompareTag("Pharmacy")){
            doTasks( "Picking up prescription which takes 15 minutes",1);
        }
        else if(other.transform.CompareTag("Grandma")){
            doTasks("Visiting grandma for 2 hours...",2);
        }
        else if(other.transform.CompareTag("Hospital")){
            doTasks( "Doctor visit which takes 1 hour...",3);
        }
        else if(other.transform.CompareTag("Supermarket")){
            doTasks( "Shopping in the supermarket for 30 minutes...",4);
        }
        else if(other.transform.CompareTag("Haircut")){
            doTasks( "Getting a haircut which takes 40 minutes...",5);
        }
        else if(other.transform.CompareTag("Car")){
            doTasks( "Car shipping for 1 hour",6);
        }
        else if(other.transform.CompareTag("Cloth")){
            doTasks( "Cloth shopping for 2 hours",7);
        }
        else if(other.transform.CompareTag("Grocery")){
            doTasks( "Buying fresh produce which takes 20 minutes",8);
        }
        else if(other.transform.CompareTag("WorkFhome")){
            doTasks( "Working from home for 4 hours...",9);
        }
    }

    public void doTasks(string m, int task){
        if(taskManager.hasTask(Tasks[task]) == true){
            doingTaskPlane.gameObject.SetActive(true);
            message.text = m;
            StartCoroutine(Wait());
            taskManager.removeTask(Tasks[task]);
            menuHandler.hideMenu();
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        doingTaskPlane.gameObject.SetActive(false);
    }
}
