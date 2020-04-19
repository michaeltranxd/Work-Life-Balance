using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public GameObject menu;

    public Button b1, b2, b3;
    public ActionButton ab1, ab2, ab3;
    public StatManager StatsManager;

    private Dictionary<string, List<Action>> database = new Dictionary<string, List<Action>>();

    public AudioSource audioSource;
    public AudioClip menuOpenSound;

    private bool shopsClosed = false;
    public Transform buildings;

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
        if (DayNightController.getDayNightController().isTimeToCloseBuidings() && !shopsClosed)
        {
            foreach (Transform child in buildings)
            {
                BoxCollider bc = child.GetComponent<BoxCollider>();
                if(bc)
                    bc.enabled = false;
            }
            shopsClosed = true;
        }
        else if (DayNightController.isDaytime() && shopsClosed)
        {
            foreach (Transform child in buildings)
            {
                BoxCollider bc = child.GetComponent<BoxCollider>();
                if (bc)
                    bc.enabled = true;
            }
            shopsClosed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Untagged") || other.tag.Equals("Bed"))
            return;

        menu.SetActive(true);
        /*
         * Get tag of collider
         * Get actions from database
         * Populate buttons based on result from database
         * Give Button an object that has information of the actions (+- which stats)
         * Enable menu
         */

        audioSource.PlayOneShot(menuOpenSound, .5f);

        string building = other.tag; //Debug.Log(other.tag);


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

        Player.showMouse();
    }

    private void OnTriggerExit(Collider other)
    {
        Player.hideMouse();
        menu.SetActive(false);
    }

    private void onButtonClick(ActionButton actionButton)
    {
        Action action = actionButton.actionToTake;
        String notEnoughStats = "";
        if (!StatsManager.EnoughTime(action.Time))
        {
            // Print you don't have enough time...
        }
        else // Check the stats
        {
            if (!StatsManager.EnoughNutri(action.Nutri))
            {
                notEnoughStats = addStatRequirementMissing(notEnoughStats, "Nutrition");
            }
            if (!StatsManager.EnoughHygiene(action.Hygiene))
            {
                notEnoughStats = addStatRequirementMissing(notEnoughStats, "Hygiene");
            }
            if (!StatsManager.EnoughEnergy(action.Energy))
            {
                notEnoughStats = addStatRequirementMissing(notEnoughStats, "Energy");
            }
            if (!StatsManager.EnoughAbility(action.Ability))
            {
                notEnoughStats = addStatRequirementMissing(notEnoughStats, "Ability");
            }
            if (!StatsManager.EnoughPhys(action.PhysHealth))
            {
                notEnoughStats = addStatRequirementMissing(notEnoughStats, "Physical Health");
            }
            if (!StatsManager.EnoughMent(action.MentHealth))
            {
                notEnoughStats = addStatRequirementMissing(notEnoughStats, "Mental Health");
            }
        }
        if (notEnoughStats.Equals(""))
        {
            StatsManager.SpendTime(action.Time);
            StatsManager.AddPhys(action.PhysHealth);
            StatsManager.AddMent(action.MentHealth);
            StatsManager.AddNutri(action.Nutri);
            StatsManager.AddEnergy(action.Energy);
            StatsManager.AddHygiene(action.Hygiene);
            StatsManager.AddAbility(action.Ability);
            print("xd");
        }
        else
        {
            // TODO add handler for insufficient stats
        }


        print(actionButton.actionToTake.name);
        Player.hideMouse();
        menu.SetActive(false);
    }

    private string actionToString(Action action)
    {
        return action.name + " - " + action.Time;
    }

    private string addStatRequirementMissing(string overall, string toAdd)
    {
        if(overall.Equals(""))
        {
            return toAdd;
        }
        return overall + ", " + toAdd;
    }

    private void parseActions()
    {
        // Read each line of the file into a string array. Each element
        // of the array is one line of the file.

        Debug.Log("ParseActions method entered");
        string[] lines = System.IO.File.ReadAllLines(Application.streamingAssetsPath + "/actiondatabase.csv");
        Debug.Log("Lines end");
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
            float e = float.Parse(splittedLine[4]);
            float h = float.Parse(splittedLine[5]);
            float ap = float.Parse(splittedLine[6]);
            string name = splittedLine[7].Trim();
            string tag = splittedLine[8].Trim(); //Debug.Log("tag: " + tag.ToString());

            Action action = new Action(time, ph, mh, n, e, h, ap, name);

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
