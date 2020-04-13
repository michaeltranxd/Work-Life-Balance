using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recap : MonoBehaviour
{
    public Stats StatsManager;
    public AudioSource audioSource;
    
    public Text statsText;

    private float old_phyHealth;
    private float old_menHealth;
    private float old_nutri;
    private float old_wake;
    private float old_energy;
    private float old_hygen;

    private float curr_phyHealth;
    private float curr_menHealth;
    private float curr_nutri;
    private float curr_wake;
    private float curr_energy;
    private float curr_hygen;

    private string actions;

    public float speed = 0.05f;
    public float tmp;
    private string fullText;
    private string currentText = "";

    private bool typing = false;
    private bool skip = false;
    // Start is called before the first frame update
    void Start()
    {
        //statsText.text = "Stats"+ "\n" + "Physical Health: " + ((int)curr_phyHealth).ToString();
        //fullText = "Stats"+ "\n" + "Physical Health: " + ((int)curr_phyHealth).ToString();
        actions = "Actions\n You haven't done anthing yet!\n";
        old_phyHealth = StatsManager.PhysHealth;
        old_menHealth = StatsManager.MentHealth;
        old_nutri = StatsManager.Nutri;
        old_hygen = StatsManager.Hygiene;
        old_energy = StatsManager.Energy;
        old_wake = StatsManager.Wake;
    }

    // Update is called once per frame
    void Update()
    {
            fullText = "Stats"+ "\n" + 
                                "Physical Health: " + ((int)old_phyHealth).ToString()+ " -> "+ ((int)curr_phyHealth).ToString() + "\n" +
                                "Mental Health: " + ((int)old_menHealth).ToString()+ " -> "+ ((int)curr_menHealth).ToString() +"\n" +
                                "Nutrition: " + ((int)old_nutri).ToString()+ " -> "+ ((int)curr_nutri).ToString() +"\n" +
                                "Wakefulness: " + ((int)old_wake).ToString()+ " -> "+ ((int)curr_wake).ToString() +"\n" +
                                "Hygiene: " + ((int)old_hygen).ToString()+ " -> "+ ((int)curr_hygen).ToString() +"\n" +
                                "Energy: " + ((int)old_energy).ToString()+ " -> "+ ((int)curr_energy).ToString() + "\n\n" +
                                actions;
        if(skip == false){
            if(typing  == true){                   
            tmp += Time.deltaTime;
            }else{
                tmp = 0;
            }
            while(tmp >= speed && currentText.Length < fullText.Length){
                currentText += fullText[currentText.Length];
                tmp -= speed;
                audioSource.Play();
            }
            if(statsText.text.Length == fullText.Length || 
                    currentText.Substring(currentText.Length - 1).Equals("\n") || 
                    currentText.Substring(currentText.Length - 1).Equals(" ")){
                audioSource.Stop();
            }
        }else{
            audioSource.Stop();
            currentText = fullText;
        }
        statsText.text = currentText;
    }

    public void skipTyping(){
        skip = true;
    }
    public void resetSkip(){
        skip = false;
    }
    public void startTyping(){
        typing = true;
    }

    public void endTyping(){
        typing = false;
    }

    public void resetCurrentText(){
        currentText = "";
        tmp = Time.deltaTime;
    }

    public void setActions(string addAction){
        if(actions.Equals("Actions\nYou haven't done anthing yet!\n")){
            actions = "Actions\n" + addAction;
        }else{
            actions += addAction;
        }
    }
    public void setCurrentPhy(float phyhealth){
        curr_phyHealth = phyhealth;
    }
    public void setCurrentMen(float menhealth){
        curr_menHealth = menhealth;
    }
    public void setCurrentNutri(float nutri){
        curr_nutri = nutri;
    }
    public void setCurrentWake(float wake){
        curr_wake = wake;
    }
    public void setCurrentEnergy(float energy){
        curr_energy = energy;
    }
    public void setCurrentHygen(float hygen){
        curr_hygen = hygen;
    }

    public void setOldPhy(float phyhealth){
        old_phyHealth = phyhealth;
    }
    public void setOldMen(float menhealth){
        old_menHealth = menhealth;
    }
    public void setOldNutri(float nutri){
        old_nutri = nutri;
    }
    public void setOldWake(float wake){
        old_wake = wake;
    }
    public void setOldHygen(float hygen){
        old_hygen = hygen;
    }
    public void setOldEnergy(float energy){
        old_energy = energy;
    }
    public void resetActions(){
        actions = "Actions\nYou haven't done anthing yet!\n";
    }
}
