using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SlideScript : MonoBehaviour
{
    private int slideNumber = 0;
    private int previousSlideNumber = -1;
    public Transform slides;
    public TextMeshProUGUI text;

    public Button PrevButton;
    public Button NextButton;

    public TextMeshProUGUI slideNumberText;

    private GameObject[] slideGameObjects;
    private int slideGameObjectIndex = 0;
    private string[] slideText = {
     /*1*/                        "Hello, this will be a tutorial over the basic UI elements and controls of the game.\n\nClick the \">\" button to advance.\nClick the \"<\" button to go back to an earlier slide\n\nAt any point you can skip the tutorial and play the game by clicking \"Start\"",
     /*2*/                        "We will be going over the UI to familiarize you with each component",
     /*3*/                        "This is the minimap. It will always be displayed and show your location on the map. You can zoom in and out using \"Page up\" and \"Page Down\" respectively",
     /*4*/                        "This is the time and day. It displays the current day and time of the game",
     /*5*/                        "These are your stats. It is the following:\n - Red -> Physical Health\n - Green -> Mental Health\n - Yellow -> Nutrition\n - Blue -> Hygiene\n - Purple -> Energy\n - Gray -> Ability Points\n\nPhysical and Mental Health are very important stats, while the other four may indirectly impact the Physical and Mental Health",
     /*6*/                        "Physical Health is how healthy you are. Your food choices and fitness have an impact on this",
     /*7*/                        "Mental Health is the state of your mind. Having too many worries from not completeing tasks have an impact on this",
     /*8*/                        "Nutrition is affected by the type of food that you eat. It is always decreasing because your body is working on keeping your body healthy so you will always need to keep eating nutritious food",
     /*9*/                        "Hygiene is affected by how clean you keep yourself. This will always be decreasing as time goes on because there are many factors such as dirt, dust, bugs, sweat, etc",
     /*10*/                       "Energy is affected by the type of action and how well you slept",
     /*11*/                       "Action Points limits you on the amount of actions that you can take per day. Sometimes we can't do every task in a day and that's how life is. We need to priortize and be efficient in our tasks",
     /*12*/                       "At the top we have the message window that will display useful information for you",
     /*13*/                       "This is the overall map. Access this using \"M\" key.",
     /*14*/                       "On the map, there are important symbols:\n - Blue colored symbols\n - Red colored symbols\n - Green colored symbols\n - Yellow colored symbols\n - White dot symbol",
     /*15*/                       "Blue colored symbols are places where you can go to perform actions. Go to the front of the building to choose your action",
     /*16*/                       "Red colored symbols are healthcare places",
     /*17*/                       "Green colored symbols are areas where you can go to relax",
     /*18*/                       "Yellow colored symbols are important areas for tasks",
     /*19*/                       "White dot symbols are displaying where you are currently are located both in the minimap and map",
     /*20*/                       "This is the task window, displaying the tasks that you are to complete. The tasks can be viewed by holding down \"Tab\" key down.",
     /*21*/                       "This is your house that you will be living in. We will go over the important aspects of the house. All activities in the house will not be as impactful compared to tasks outside",
     /*22*/                       "The bed is for you to sleep to pass time and regain your ability points and energy for the next day. \n\nBe weary that if you sleep while having tasks, you will get penalized! Also, sleep before midnight!!",
     /*23*/                       "The bathroom is for you to practice good hygiene and clean yourself",
     /*24*/                       "The kitchen is for you to get food for when buildings are closed",
     /*25*/                       "The gym is for you to get quick workouts when buildings are closed",
     /*26*/                       "The couch is for you to relax and relieve your mental stress",
     /*27*/                       "The desk is another way for you to relax and relieve your mental stress. In addition, you will need to work from home at this desk",
     /*28*/                       "This is the pause menu. Press \"Escape\" key at any point in the game to have this menu",
     /*29*/                       "You can change the volume settings in here",
     /*30*/                       "You can save and quit the game here",
     /*31*/                       "Walk up to buildings if you would like to be prompted for actions. The following window will pop up with the actions",
     /*32*/                       "Press 'H' key for any questions regarding the controls. It will display the controls of the game",
     /*33*/                       "Now that is all for the tutorial, hopefully you have a better grasp of the game. \n\nSome tips before you start:\n - Try to complete all tasks before you sleep\n - Prevent any of your stats from reaching zero\n - Always go to sleep before midnight!\n - Game will save everytime you go to sleep",
     /*34*/                       "Story: You have just been discharged from the hospital after a distressing few days and your doctor’s have told you that you will need to start new medications and significantly change your lifestyle habits to maintain your health. Your main objective for the next 30 days is to recover, establish new healthy habits, find a good work life balance, and prevent getting readmitted into the hospital. Explore your community and utilize all the resources available to reach your new health goals!\n\nPress \"Start\" to begin"
    };

    // Start is called before the first frame update
    void Start()
    {
        slideGameObjects = new GameObject[slides.childCount];
        foreach(Transform child in slides)
        {
            slideGameObjects[slideGameObjectIndex] = child.gameObject;
            slideGameObjectIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (previousSlideNumber != slideNumber)
        {
            if (slideNumber == 0)
                // First slide so disable prev
                PrevButton.interactable = false;
            if (slideNumber == slideGameObjects.Length - 1)
            {
                NextButton.interactable = false;
                // Make start visible
            }
            if(slideNumber > 0 && slideNumber < slideGameObjects.Length - 1)
            {
                PrevButton.interactable = true;
                NextButton.interactable = true;
            }

            slideNumberText.text = slideNumber + 1 + "/" + slideGameObjects.Length;

            previousSlideNumber = slideNumber;

            for (int i = 0; i < slideGameObjects.Length; i++)
            {
                if (i == slideNumber)
                {
                    slideGameObjects[slideNumber].SetActive(true);
                    text.text = slideText[i];
                }
                else
                    slideGameObjects[i].SetActive(false);
            }
        }
    }

    public void onNextClick()
    {
        slideNumber++;
    }

    public void onPrevClick()
    {
        slideNumber--;
    }
}
