using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour
{

    public Light sun;
    public Light moon;
    public float secondsInFullDay = 1200f;
    public float currentTimeOfDay = 0;
    public float timeMultiplier = 1f;

    public const float startOfSunrise = .25f;
    public const float startOfDaytime = .35f;
    public const float startOfSunset = .70f; 
    public const float startOfNighttime = .80f;

    float sunInitialIntensity;
    private int numDays = 0;
    private float currentHour = 0f;
    private float currentMinute = 0f;

    void Start()
    {
        sunInitialIntensity = sun.intensity;
    }

    bool isSunrise()
    {
        return currentTimeOfDay >= startOfSunrise && currentTimeOfDay < startOfDaytime;
    }

    bool isDaytime()
    {
        return currentTimeOfDay >= startOfDaytime && currentTimeOfDay < startOfSunset;
    }
    bool isSunset()
    {
        return currentTimeOfDay >= startOfSunset && currentTimeOfDay < startOfNighttime;
    }
    bool isNighttime()
    {
        return currentTimeOfDay >= startOfNighttime || currentTimeOfDay < startOfSunrise;
    }

    void Update()
    {
        UpdateSunAndMoon();

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        // Digital time
        currentHour = 24 * currentTimeOfDay;
        currentMinute = 60 * (currentHour - Mathf.Floor(currentHour));

        print((int)currentHour + ": " + (int)currentMinute);

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
            numDays++;
        }
    }

    void UpdateSunAndMoon()
    {
        // This rotates the sun and moon 360 degree in X-axis according to our current time of day.
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360) - 90, 170, 0);
        moon.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360) - 270, 170, 0);

        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
       // moon.intensity = sunInitialIntensity * intensityMultiplier;
    }
}