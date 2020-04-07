using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    //public Text stats;
    public Text message;
    public Text TimeText;

    // set to public for testing purposes
    public float Timeleft;
    private float TimeWasLeft;
    public float PhysHealth;
    public float MentHealth;
    public float Nutri;
    public float Hygiene;
    public float Energy;
    public float Wake;

    public int ThirstOverTime;
    public int HungerOverTime;

    public Slider PhysHealthBar;
    public Slider MentHealthBar;
    public Slider NutriBar;
    public Slider HygieneBar;
    public Slider EnergyBar;
    public Slider WakeBar;

    // set to public for testing purposes

    public DayNightController dayNightController;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        PhysHealthBar.maxValue = PhysHealth;
        MentHealthBar.maxValue = MentHealth;
        NutriBar.maxValue = Nutri;
        HygieneBar.maxValue = Hygiene;
        EnergyBar.maxValue = Energy;
        WakeBar.maxValue = Wake;
        TimeWasLeft = Timeleft;

    }

    // Update is called once per frame
    void Update()
    {
        Timeleft = dayNightController.timeLeft();
        Nutri -= HungerOverTime * (Time.deltaTime)/60;
        Hygiene -= ThirstOverTime * (Time.deltaTime)/60;
        Nutri -= HungerOverTime * Time.deltaTime / 60 * DayNightController.timeMultiplier;    // Every minute = 1 point
        Hygiene -= ThirstOverTime * Time.deltaTime / 60 * DayNightController.timeMultiplier;  // Every minute = 1 point

        UpdateUI();
    }

    public void UpdateUI(){
        PhysHealthBar.value = PhysHealth;
        MentHealthBar.value = MentHealth;
        NutriBar.value = Nutri;
        HygieneBar.value = Hygiene;
        WakeBar.value = Wake;
        EnergyBar.value = Energy;
        if (PhysHealth == 0){
            message.text = "You have been sent back to the hospital";
        }
        else if(MentHealth == 0){
            message.text = "You have been sent back to the hospital";
        }
        if (dayNightController.isSkippingNight())
        {
            message.text = "Sleeping for the day";
        }
        else if (dayNightController.isInEvent())
        {
            message.text = "Doing event...";
        }
        else if (dayNightController.isCloseToSleep())
        {
            message.text = "You should sleep soon";
        }
        else if (dayNightController.isPastSleep())
        {
            message.text = "You feel very tired";
            print("past sleep");
            // TODO, reset day and send user back home
        }
        else if (DayNightController.isNighttime())
        {
            message.text = "It is night time";
        }
        else
        {
            message.text = "Start doing actions";
        }
        TimeText.text = "Time: " + dayNightController.getCurrentHour() + ":" + dayNightController.getCurrentMinute();

    }
    public void SpendTime(float amount)
    { 
        TimeWasLeft = Timeleft;
        Timeleft -= amount;
        print(Timeleft);

        player.playerSkipTime(amount);
    }

    public bool EnoughTime(float amount)
    {
        return Timeleft - amount > 0;
    }

    public void AddPhys(float amount)
    {
        PhysHealth = Mathf.Clamp(PhysHealth + amount, 0, PhysHealthBar.maxValue);
    }

    public void AddMent(float amount)
    {
        MentHealth = Mathf.Clamp(MentHealth + amount, 0, MentHealthBar.maxValue);
    }

    public void AddNutri(float amount)
    {
        Nutri = Mathf.Clamp(Nutri + amount, 0, NutriBar.maxValue);
    }

    public void AddWake(float amount)
    {
        Wake = Mathf.Clamp(Wake + amount, 0, WakeBar.maxValue);
    }

    public void AddHygiene(float amount)
    {
        Hygiene = Mathf.Clamp(Hygiene + amount, 0, HygieneBar.maxValue);
    }
    public void AddEnergy(float amount)
    {
        Energy = Mathf.Clamp(Energy + amount, 0, EnergyBar.maxValue);
    }

}
