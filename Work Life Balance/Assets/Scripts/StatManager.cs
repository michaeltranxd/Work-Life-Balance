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
    int EnergyOverTime = 1;

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

    private string messageString;
    public static bool GameOver = false;
    public AudioSource SoundManager;
    public AudioClip notificationSound;

    private float statDecreaseMultiplier = 4f;

    private bool existingData = false;

    void Awake()
    {
        for (int i = 0; i < barNames.Length; i++)
        {
            bars[i] = statsUI.transform.Find(barNames[i]).GetComponent<Slider>();
            barTexts[i] = statsUI.transform.Find(barNames[i]).Find(dataNames[i]).GetComponent<Text>();
            bars[i].maxValue = MaxValues[i];
            barTexts[i].text = ((int)MaxValues[i]).ToString();
        }
        stats = new Stats(MaxValues[0], MaxValues[1], MaxValues[2], MaxValues[3], MaxValues[4], MaxValues[5]);

        if (LevelLoader.LoadingSavedFile)
            LoadStat();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Allow recap to grab our intiial configurations
        recap.setInitialConfiguartion();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver || PauseManager.GamePaused)
            return;
        if (DayNightController.GameWon)
        {
            GameOverText.text = "You won!";
            GameOverPanle.gameObject.SetActive(true);
            return;
        }
        Timeleft = dayNightController.timeLeft();
        stats.Nutri -= HungerOverTime * statDecreaseMultiplier * Time.deltaTime / 60 * DayNightController.timeMultiplier;    // Every minute = 1 point
        stats.Hygiene -= ThirstOverTime * statDecreaseMultiplier * Time.deltaTime / 60 * DayNightController.timeMultiplier;  // Every minute = 1 point
        stats.Energy -= EnergyOverTime * statDecreaseMultiplier * Time.deltaTime / 60 * DayNightController.timeMultiplier;  // Every minute = 1 point
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
        if(stats.Energy <= 0){
            stats.PhysHealth -= Time.deltaTime / 60 * DayNightController.timeMultiplier;
            Player.fatigued();
            if(stats.Energy < 0){
                stats.Energy = 0;
            }
        }
        else
        {
            Player.noFatigued();
        }
        if(stats.MentHealth <= 0 || stats.PhysHealth <= 0){
            GameOverText.text = "Game Over";
            GameOverPanle.gameObject.SetActive(true);
            GameOver = true;
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
            messageString = "You have been sent back to the hospital";
        }
        else if(stats.MentHealth == 0){
            messageString = "You have been sent back to the hospital";
        }
        else if (dayNightController.isInEvent())
        {
            messageString = "Doing event...";
        }
        else if (dayNightController.isCloseToSleep())
        {
            messageString = "It almost midnight...";
        }
        else if (dayNightController.isOneHourBeforeCloseBuilding())
        {
            messageString = "Buildings are closed in one hour";
        }
        else if (dayNightController.isTimeToCloseBuidings())
        {
            messageString = "Buildings are closed... Return home";
            // TODO, reset day and send user back home
        }
        else if (DayNightController.isNighttime())
        {
            messageString = "It is night time";
        }
        else
        {
            messageString = "Start doing actions";
        }

        if (!message.text.Equals(messageString) && !message.text.Equals("Timmy gave you a cookie."))
        {
            messagePlane.gameObject.SetActive(true);
            message.text = messageString;
            SoundManager.PlayOneShot(notificationSound);
            StartCoroutine(Wait());
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
        Timeleft -= amount;
        print("Time" + Timeleft);

        player.playerSkipTime(amount);
    }

    public bool EnoughTime(float amount)
    {
        return Timeleft + amount > 0;
    }

    public void AddPhys(float amount)
    {
        stats.PhysHealth = Mathf.Clamp(stats.PhysHealth + amount, 0, bars[0].maxValue);
    }

    public bool EnoughPhys(float amount)
    {
        return stats.PhysHealth + amount > 0;
    }

    public void AddMent(float amount)
    {
        stats.MentHealth = Mathf.Clamp(stats.MentHealth + amount, 0, bars[1].maxValue);
    }

    public bool EnoughMent(float amount)
    {
        return stats.MentHealth + amount > 0;
    }

    public void AddNutri(float amount)
    {
        stats.Nutri = Mathf.Clamp(stats.Nutri + amount, 0, bars[2].maxValue);
    }

    public bool EnoughNutri(float amount)
    {
        return stats.Nutri + amount > 0;
    }

    public void AddHygiene(float amount)
    {
        stats.Hygiene = Mathf.Clamp(stats.Hygiene + amount, 0, bars[3].maxValue);
    }

    public bool EnoughHygiene(float amount)
    {
        return stats.Hygiene + amount > 0;
    }

    public void AddEnergy(float amount)
    {
        stats.Energy = Mathf.Clamp(stats.Energy + amount, 0, bars[4].maxValue);
    }

    public bool EnoughEnergy(float amount)
    {
        return stats.Energy + amount > 0;
    }

    public void AddAbility(float amount)
    {
        stats.Ability = Mathf.Clamp(stats.Ability + amount, 0, bars[5].maxValue);
    }

    public bool EnoughAbility(float amount)
    {
        return stats.Ability + amount > 0;
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

    public void LoadStat()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        stats.PhysHealth = data.stats[0];
        stats.MentHealth = data.stats[1];
        stats.Nutri = data.stats[2];
        stats.Hygiene = data.stats[3];
        stats.Energy = data.stats[4];
        stats.Ability = data.stats[5];
    }

}
