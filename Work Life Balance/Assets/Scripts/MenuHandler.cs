using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public GameObject menu;

    public Button b1, b2, b3;
    public ActionButton ab1, ab2, ab3;
    public Stats StatsManager;

    private Dictionary<string, List<Action>> database = new Dictionary<string, List<Action>>();
    
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
        parseActions();
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

        // Access the actions in the database using the code below
        List<Action> actions;
        if (database.TryGetValue(other.tag, out actions))
        {
            ab1.actionToTake = actions[0];
            b1.GetComponentInChildren<Text>().text = actionToString(actions[0]);
            ab2.actionToTake = actions[1];
            b2.GetComponentInChildren<Text>().text = actionToString(actions[1]);
            ab3.actionToTake = actions[2];
            b3.GetComponentInChildren<Text>().text = actionToString(actions[2]);
        }

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

    private string actionToString(Action action)
    {
        return action.name + " - " + action.Time;
    }

    private void parseActions()
    {
        // Read each line of the file into a string array. Each element
        // of the array is one line of the file.
        string[] lines = System.IO.File.ReadAllLines(Application.dataPath + "/StreamingAssets" + "/actiondatabase.txt");
        
        foreach(string line in lines)
        {
            if (line.StartsWith("//"))
            {
                // Skip comments
                continue;
            }

            // Parse each line into respective attributes
            string[] splittedLine = line.Split(',');
            if(splittedLine.Length != 9)
            {
                print("This line could not be parsed: " + line);
                continue;
            }
            float time = float.Parse(splittedLine[0]);
            float ph = float.Parse(splittedLine[1]);
            float mh = float.Parse(splittedLine[2]);
            float n = float.Parse(splittedLine[3]);
            float w = float.Parse(splittedLine[4]);
            float h = float.Parse(splittedLine[5]);
            float e = float.Parse(splittedLine[6]);
            string name = splittedLine[7].Trim();
            string tag = splittedLine[8].Trim();

            Action action = new Action(time, ph, mh, n, w, h, e, name);

            List<Action> actions;
            if (!database.TryGetValue(tag, out actions))
            {
                // Database does not have a key to list entry, we need to create one
                actions = new List<Action>();
                actions.Add(action);
                database.Add(tag, actions);
            }
            else
            {
                actions.Add(action);
                database[tag] = actions;
            }
                                          
        }
    }
}
