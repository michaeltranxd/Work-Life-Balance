using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour
{
    public Light sun;
    public Light moon;
    private static float minutesInFullDay = 1200f;
    private static float currentTimeOfDay = .80f;
    public static float timeMultiplier = 1f;

    public const float startOfSunrise = .25f;
    public const float startOfDaytime = .35f;
    public const float startOfSunset = .65f; 
    public const float startOfNighttime = .80f;
    public const float startOfMidnight = 0f;
    float startOfNoon = .5f;

    float sunIntensity = 1;
    float moonIntensity = 1;

    // For the sun intensities
    float daytimeMaxIntensity = .7f;
    float sunriseMaxIntesity = .3f;

    // For the moon intensities
    float nighttimeMaxIntensity = .7f;
    float sunsetMaxIntensity = .3f;

    private static int numDays = 0;
    private static float currentHour = 0f;
    private static float currentMinute = 0f;

    private static Event currentEvent = null;

    static bool isSunrise()
    {
        return currentTimeOfDay >= startOfSunrise && currentTimeOfDay < startOfDaytime;
    }

    public static bool isDaytime()
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

    public bool isCloseToSleep()
    {
        return currentTimeOfDay < .90f && currentTimeOfDay > .85f;
    }
    public bool isPastSleep()
    {
        return currentTimeOfDay >= .90f;
    }
    public float timeLeft()
    {
        return (.90f - currentTimeOfDay) * 24f * 60f;
    }

    void Update()
    {
        UpdateSunAndMoon();

        currentTimeOfDay += (Time.deltaTime / minutesInFullDay) * timeMultiplier;
        //print(currentTimeOfDay);
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
            else if (currentEvent is SkipTimeEvent anEvent)
            {
                // 1 hr / 24hrs is ratio, since we are in minutes we / 60 / 24, if we were 1 hr we do / 24
                if (currentTimeOfDay >= (anEvent.initialTime + anEvent.minutesToSkip / 60 / 24))
                {
                    timeMultiplier = 1f;
                    currentEvent.end();
                    currentEvent = null;
                }
            }
        }
    }

    public int getCurrentHour()
    {
        currentHour = 24 * currentTimeOfDay;
        return (int)currentHour;
    }

    public int getCurrentMinute()
    {
        currentMinute = 60 * (currentHour - Mathf.Floor(currentHour));
        return (int)currentMinute;
    }


    void UpdateSunAndMoon()
    {
        // This rotates the sun and moon 360 degree in X-axis according to our current time of day.
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360) - 90, 170, 0);
        moon.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360) - 270, 170, 0);

        if (isNighttime())
        {
            sunIntensity = 0;
            if(currentTimeOfDay > startOfNighttime)
                moonIntensity = Mathf.Clamp01(map(currentTimeOfDay, startOfNighttime, .99f, sunsetMaxIntensity, sunsetMaxIntensity + (nighttimeMaxIntensity / 2)));
        }
        else if (isSunrise())
        {
            sunIntensity = Mathf.Clamp01(map(currentTimeOfDay, startOfSunrise, startOfDaytime, 0, sunriseMaxIntesity));
            moonIntensity = Mathf.Clamp01(sunsetMaxIntensity + (nighttimeMaxIntensity / 2) - map(currentTimeOfDay, startOfSunrise, startOfDaytime, 0, sunsetMaxIntensity + (nighttimeMaxIntensity / 2)));
        }
        else if (isSunset())
        {
            sunIntensity = Mathf.Clamp01(startOfNighttime - map(currentTimeOfDay, startOfSunset, startOfNighttime, 0f, daytimeMaxIntensity));
            moonIntensity = Mathf.Clamp01(map(currentTimeOfDay, startOfSunset, startOfNighttime, 0, sunsetMaxIntensity));
        }
        else if (isDaytime())
        {
            sunIntensity = Mathf.Clamp01(map(currentTimeOfDay, startOfDaytime, startOfNoon, sunriseMaxIntesity, daytimeMaxIntensity));
            moonIntensity = 0f;
        }
        sun.intensity = sunIntensity;
        moon.intensity = moonIntensity;
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

    public static bool CanSkipTime(float minutes)
    {
        print(currentTimeOfDay + minutes / minutesInFullDay);
        return currentTimeOfDay + minutes / minutesInFullDay > startOfDaytime && (currentTimeOfDay + minutes / minutesInFullDay) < startOfNighttime;
    }
    public static void SkipTime(SkipTimeEvent e)
    {
        e.initialTime = currentTimeOfDay;
        currentEvent = e;
        timeMultiplier = 40f;
    }

    /* Function will remap range of [a,b] to new range [c,d]
     * and return the value that you want mapped from [a, b] to [c,d]
     * 
     * Credit to Eric5h5
     * https://forum.unity.com/threads/mapping-or-scaling-values-to-a-new-range.180090/
     */
    private float map(float value, float a, float b, float c, float d)
    {
        return Mathf.Lerp(c, d, Mathf.InverseLerp(a, b, value));
    }
}