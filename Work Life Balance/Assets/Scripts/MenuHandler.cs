using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public GameObject menu;

    public Button b1, b2, b3;
    public ActionButton ab1, ab2, ab3;
    public Stats StatsManager;

    
    // Start is called before the first frame update
    void Start()
    {
        ab1 = new ActionButton();
        ab2 = new ActionButton();
        ab3 = new ActionButton();

        b1.onClick.AddListener(() =>
        {
            onButtonClick(ab1);
        });
        b2.onClick.AddListener(() =>
        {
            onButtonClick(ab2);
        });
        b3.onClick.AddListener(() =>
        {
            onButtonClick(ab3);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        menu.SetActive(true);
        /*
         * Get tag of collider
         * Get actions from database
         * Populate buttons based on result from database
         * Give Button an object that has information of the actions (+- which stats)
         * Enable menu
         */

        /* Testing code */
        Action a1 = new Action(100, -20, 0, 0, 0, 20, 50, "Run on Treadmill");
        Action a2 = new Action(1, -50, -50, -100, -100, -200, -200, "3");
        Action a3 = new Action(100, -30, 0, 50, 0, 20, 0, "6");
        ab1.actionToTake = a1;
        ab2.actionToTake = a2;
        ab3.actionToTake = a3;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnTriggerExit(Collider other)
    {
        menu.SetActive(false);
    }

    private void onButtonClick(ActionButton actionButton)
    {
        Action action = actionButton.actionToTake;
        if (StatsManager.EnoughTime(action.Time))
        {
            StatsManager.SpendTime(action.Time);
            StatsManager.AddPhys(action.PhysHealth);
            StatsManager.AddMent(action.MentHealth);
            StatsManager.AddNutri(action.Nutri);
            StatsManager.AddWake(action.Wake);
            StatsManager.AddHygiene(action.Hygiene);
            StatsManager.AddEnergy(action.Energy);
        }
        else
        {
            // Handle time issues
            print("not enough time");
        }
        print(actionButton.actionToTake.name);
        menu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
