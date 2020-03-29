using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public Text stats;
    public Text message;
    public Text TimeText;

    // set to public for testing purposes
    public int Timeleft;
    private int TimeWasLeft;
    public int PhysHealth;
    public int MentHealth;
    public float Nutri;
    public float Hygiene;
    public int Energy;
    public int Wake;

    public int ThirstOverTime;
    public int HungerOverTime;

    public Slider PhysHealthBar;
    public Slider MentHealthBar;
    public Slider NutriBar;
    public Slider HygieneBar;
    public Slider EnergyBar;
    public Slider WakeBar;

    // set to public for testing purposes

    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        PhysHealthBar.maxValue = PhysHealth;
        MentHealthBar.maxValue = MentHealth;
        TimeWasLeft = Timeleft;

    }

    // Update is called once per frame
    void Update()
    {
        Nutri -= HungerOverTime * (Time.deltaTime)/60;
        Hygiene -= ThirstOverTime * (Time.deltaTime)/60;
        if(TimeWasLeft != Timeleft){
            Debug.Log(TimeWasLeft - Timeleft);
            Nutri -= HungerOverTime * (TimeWasLeft - Timeleft);
            Hygiene -= ThirstOverTime * (TimeWasLeft - Timeleft);
            TimeWasLeft = Timeleft;
        }

        UpdateUI();
    }

    public void UpdateUI(){
        if(PhysHealth < 0){
            message.text = "You have died";
        }
        else if(PhysHealth > PhysHealthBar.maxValue){
            PhysHealth = (int)PhysHealthBar.maxValue;
        }
        else if(MentHealth < 0){
            message.text = "You went crazy";
        }
        else if(MentHealth > MentHealthBar.maxValue){
            MentHealth = (int)MentHealthBar.maxValue;
        }
        else if(Timeleft < 0){
            TimeText.text = "Time: 0";
            message.text = "Time's up";
        }
        else{
            PhysHealthBar.value = PhysHealth;
            MentHealthBar.value = MentHealth;
            TimeText.text = "Time: " + Timeleft.ToString();
            NutriBar.value = Nutri;
            HygieneBar.value = Hygiene;
            WakeBar.value = Wake;
            EnergyBar.value = Energy;
        }
    }
    public void SpendTime(int amount)
    {
        TimeWasLeft = Timeleft;
        Timeleft -= amount;
    }

    public void DecrementPhys(int amount)
    {
        PhysHealth -= amount;
    }

    public void IncrementPhys(int amount)
    {
        PhysHealth += amount;
    }

    public void DecrementMent(int amount)
    {
        MentHealth -= amount;
    }

    public void IncrementMent(int amount)
    {
        MentHealth += amount;
    }

    public void DecrementNutri(float amount)
    {
        Nutri += amount;
    }
    public void IncrementNutri(float amount)
    {
        Nutri += amount;
    }

    public void DecrementWake(int amount)
    {
        Wake += amount;
    }
    public void IncrementWake(int amount)
    {
        Wake += amount;
    }

    public void DecrementHygiene(float amount)
    {
        Hygiene += amount;
    }
    public void IncrementHygiene(float amount)
    {
        Hygiene += amount;
    }

    public void DecrementEnergy(int amount)
    {
        Energy += amount;
    }
    public void IncrementEnergy(int amount)
    {
        Energy += amount;
    }

}
