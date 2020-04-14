using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    private class Stats
    {
        public Stats(float PH, float MH, float N, float H, float E, float AP)
        {
            this.PhysHealth = PH;
            this.MentHealth = MH;
            this.Nutri = N;
            this.Hygiene = H;
            this.Energy = E;
            this.Ability = AP;
        }
        public float PhysHealth
        { get; set; }
        public float MentHealth
        { get; set; }
        public float Nutri
        { get; set; }
        public float Hygiene
        { get; set; }
        public float Energy
        { get; set; }
        public float Ability
        { get; set; }
    }

    //public Text stats;
    public Text message;
    public Text TimeText;
    public Text GameOverText;

    private Text[] barTexts = new Text[6];

    // set to public for testing purposes
    public float Timeleft;
    private float TimeWasLeft;
    public float[] MaxValues = new float[6];

    public int ThirstOverTime;
    public int HungerOverTime;

    private Slider[] bars = new Slider[6];
    // 0 - Physical Health Bar
    // 1 - Mental Health Bar
    // 2 - Nutrition Bar
    // 3 - Hygiene Bar
    // 4 - Energy Bar
    // 5 - Ability Bar

    public RectTransform GameOverPanle;
    public RectTransform messagePlane;

    public Recap recap;
    // set to public for testing purposes

    public DayNightController dayNightController;
    public Player player;

    public GameObject statsUI; // Gameobject holding the stats UI

    private Stats stats;

    private string[] barNames = { "PhyHealthBar", "MentHealthBar", "NutriBar", "HygieneBar", "EnergyBar", "AbilityBar" };
    private string[] dataNames = { "PhyData", "MentData", "NutriData", "HygData", "EnergyData", "AbilityData" };

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < barNames.Length; i++)
        {
            bars[i] = statsUI.transform.Find(barNames[i]).GetComponent<Slider>();
            barTexts[i] = statsUI.transform.Find(barNames[i]).Find(dataNames[i]).GetComponent<Text>();
            bars[i].maxValue = MaxValues[i];
            barTexts[i].text = ((int)MaxValues[i]).ToString();
        }
        stats = new Stats(MaxValues[0], MaxValues[1], MaxValues[2], MaxValues[3], MaxValues[4], MaxValues[5]);

        // Allow recap to grab our intiial configurations
        recap.setInitialConfiguartion();
    }

    // Update is called once per frame
    void Update()
    {
        Timeleft = dayNightController.timeLeft();
        stats.Nutri -= HungerOverTime * Time.deltaTime / 60 * DayNightController.timeMultiplier;    // Every minute = 1 point
        stats.Hygiene -= ThirstOverTime * Time.deltaTime / 60 * DayNightController.timeMultiplier;  // Every minute = 1 point
        checkStats();
        UpdateUI();
    }

    public void checkStats(){
        if(stats.Nutri <=0){
            stats.PhysHealth -= Time.deltaTime / 60 * DayNightController.timeMultiplier; //1 point per mins
            if(stats.Nutri < 0){
                stats.Nutri = 0;
            }
        }
        if(stats.Hygiene <= 0){
            stats.PhysHealth -= Time.deltaTime / 60 * DayNightController.timeMultiplier;
            if(stats.Hygiene < 0){
                stats.Hygiene = 0;
            }
        }
        if(stats.Ability <= 0){ // Might not need this
            stats.MentHealth -= Time.deltaTime / 60 * DayNightController.timeMultiplier;
            if(stats.Ability < 0){
                stats.Ability = 0;
            }
        }
        if(stats.Energy <= 0){
            stats.PhysHealth -= Time.deltaTime / 60 * DayNightController.timeMultiplier;
            if(stats.Energy < 0){
                stats.Energy = 0;
            }
        }
        if(stats.MentHealth <= 0 || stats.PhysHealth <= 0){
            GameOverText.text = "Game Over";
            GameOverPanle.gameObject.SetActive(true);
        }
    }
    public void UpdateUI(){

        //set statsBar values
        bars[0].value = stats.PhysHealth;
        bars[1].value = stats.MentHealth;
        bars[2].value = stats.Nutri;
        bars[3].value = stats.Hygiene;
        bars[4].value = stats.Energy;
        bars[5].value = stats.Ability;
        barTexts[0].text = ((int)stats.PhysHealth).ToString();
        barTexts[1].text = ((int)stats.MentHealth).ToString();
        barTexts[2].text = ((int)stats.Nutri).ToString();
        barTexts[3].text = ((int)stats.Hygiene).ToString();
        barTexts[4].text = ((int)stats.Energy).ToString();
        barTexts[5].text = ((int)stats.Ability).ToString();

        //update currentPhy to recap
        recap.setCurrentPhy(stats.PhysHealth);
        recap.setCurrentMen(stats.MentHealth);
        recap.setCurrentNutri(stats.Nutri);
        recap.setCurrentHygen(stats.Hygiene);
        recap.setCurrentAbility(stats.Ability);
        recap.setCurrentEnergy(stats.Energy);

        if (stats.PhysHealth == 0){
            string m = "You have been sent back to the hospital";
            if(! message.text.Equals(m)){
                messagePlane.gameObject.SetActive(true);
                message.text = m;
                StartCoroutine(Wait());
            }
        }
        else if(stats.MentHealth == 0){
            string m = "You have been sent back to the hospital";
            if(! message.text.Equals(m)){
                messagePlane.gameObject.SetActive(true);
                message.text = m;
                StartCoroutine(Wait());
            }
        }
        else if (dayNightController.isInEvent())
        {
            string m = "Doing event...";
            if(! message.text.Equals(m)){
                messagePlane.gameObject.SetActive(true);
                message.text = m;
                StartCoroutine(Wait());
            }
        }
        else if (dayNightController.isCloseToSleep())
        {
            string m = "You should sleep soon";
            if(! message.text.Equals(m)){
                messagePlane.gameObject.SetActive(true);
                message.text = m;
                StartCoroutine(Wait());
            }
        }
        else if (dayNightController.isPastSleep())
        {
            string m = "You feel very tired";
            if(! message.text.Equals(m)){
                messagePlane.gameObject.SetActive(true);
                message.text = m;
                StartCoroutine(Wait());
            }
            print("past sleep");
            // TODO, reset day and send user back home
        }
        else if (DayNightController.isNighttime())
        {
            string m = "It is night time";
            if(! message.text.Equals(m)){
                messagePlane.gameObject.SetActive(true);
                message.text = m;
                StartCoroutine(Wait());
            }
        }
        else
        {
            string m = "Start doing actions";
            if(! message.text.Equals(m)){
                messagePlane.gameObject.SetActive(true);
                message.text = m;
                StartCoroutine(Wait());
            }
        }
        TimeText.text = "Time: " + dayNightController.getCurrentHour() + ":" + dayNightController.getCurrentMinute();

        //record the Stats when start a new day
        /*if(dayNightController.getCurrentHour() == 8 && dayNightController.getCurrentMinute() == 24){
            recap.setOldPhy(PhysHealth);
        }*/

    }

    public void resetRecap(){
        Debug.Log("resetRecap" + stats.PhysHealth);
        recap.setOldPhy(stats.PhysHealth);
        recap.setOldMen(stats.MentHealth);
        recap.setOldNutri(stats.Nutri);
        recap.setOldAbility(stats.Ability);
        recap.setOldHygen(stats.Hygiene);
        recap.setOldEnergy(stats.Energy);
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
        stats.PhysHealth = Mathf.Clamp(stats.PhysHealth + amount, 0, bars[0].maxValue);
    }

    public void AddMent(float amount)
    {
        stats.MentHealth = Mathf.Clamp(stats.MentHealth + amount, 0, bars[1].maxValue);
    }

    public void AddNutri(float amount)
    {
        stats.Nutri = Mathf.Clamp(stats.Nutri + amount, 0, bars[2].maxValue);
    }

    public void AddHygiene(float amount)
    {
        stats.Hygiene = Mathf.Clamp(stats.Hygiene + amount, 0, bars[3].maxValue);
    }

    public void AddAbility(float amount)
    {
        stats.Ability = Mathf.Clamp(stats.Ability + amount, 0, bars[4].maxValue);
    }

    public void AddEnergy(float amount)
    {
        stats.Energy = Mathf.Clamp(stats.Energy + amount, 0, bars[5].maxValue);
    }


    public float GetPhys()
    {
        return stats.PhysHealth;
    }

    public float GetMent()
    {
        return stats.MentHealth;
    }

    public float GetNutri()
    {
        return stats.Nutri;
    }

    public float GetHygiene()
    {
        return stats.Hygiene;
    }

    public float GetAbility()
    {
        return stats.Ability;
    }

    public float GetEnergy()
    {
        return stats.Energy;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        messagePlane.gameObject.SetActive(false);
    }

}
