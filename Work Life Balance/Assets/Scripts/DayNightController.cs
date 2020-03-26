using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour
{
    public Light sun;
    public Light moon;
    private static float secondsInFullDay = 1200f;
    private static float currentTimeOfDay = 0;
    private static float timeMultiplier = 1f;

    public const float startOfSunrise = .25f;
    public const float startOfDaytime = .35f;
    public const float startOfSunset = .70f; 
    public const float startOfNighttime = .80f;

    float sunInitialIntensity;
    private static int numDays = 0;
    private static float currentHour = 0f;
    private static float currentMinute = 0f;

    private static Event currentEvent = null;

    void Start()
    {
        sunInitialIntensity = sun.intensity;
    }

    static bool isSunrise()
    {
        return currentTimeOfDay >= startOfSunrise && currentTimeOfDay < startOfDaytime;
    }

    static bool isDaytime()
    {
        return currentTimeOfDay >= startOfDaytime && currentTimeOfDay < startOfSunset;
    }
    static bool isSunset()
    {
        return currentTimeOfDay >= startOfSunset && currentTimeOfDay < startOfNighttime;
    }
    static bool isNighttime()
    {
        return currentTimeOfDay >= startOfNighttime || currentTimeOfDay < startOfSunrise;
    }

    void Update()
    {
        UpdateSunAndMoon();

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
            numDays++;
        }

        if(currentEvent != null)
        {
            if(currentEvent is SleepEvent && isDaytime())
            {
                timeMultiplier = 1f;
                currentEvent.end();
                currentEvent = null;
            }
        }
    }

    void getCurrentTime()
    {
        // Digital time
        currentHour = 24 * currentTimeOfDay;
        currentMinute = 60 * (currentHour - Mathf.Floor(currentHour));

        //print((int)currentHour + ": " + (int)currentMinute);
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

    public static bool CanSkipNighttime()
    {
        return isNighttime();
    }
    public static void SkipNighttime(Event e)
    {
        currentEvent = e;
        timeMultiplier = 40f;
    }
}