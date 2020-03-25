using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public Text stats;

    // set to public for testing purposes
    public int Time;
    public int PhysHealth;
    public int MentHealth;

    // set to public for testing purposes

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stats.text = "Time: " + Time.ToString() +
                        "\nPhysical: " + PhysHealth.ToString() +
                        "\nMental: " + MentHealth.ToString();
    }

    public void SpendTime(int amount)
    {
        Time -= amount;
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

}
