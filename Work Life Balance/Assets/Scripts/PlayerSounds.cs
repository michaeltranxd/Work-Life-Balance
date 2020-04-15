using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource FootstepsSoundManager;
    public AudioSource BackgroundSoundManager;
    public AudioClip[] footStepSounds;
    public AudioClip daytimeSound;
    public AudioClip nighttimeSound;
    
    void Start()
    {
        
    }
    public void printMe(string s)
    {
        int randomIndex = Random.Range(0, footStepSounds.Length);
        FootstepsSoundManager.PlayOneShot(footStepSounds[randomIndex], .3f);
        //Debug.Log("PrintEvent: " + s + " called at: " + Time.time);
    }

    public void playDaytimeSounds()
    {
        BackgroundSoundManager.Stop();
        BackgroundSoundManager.clip = daytimeSound;
        BackgroundSoundManager.Play();
    }

    public void playNighttimeSounds()
    {
        BackgroundSoundManager.Stop();
        BackgroundSoundManager.clip = nighttimeSound;
        BackgroundSoundManager.Play();
    }
}
