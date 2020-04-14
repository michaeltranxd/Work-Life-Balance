using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource SoundManager;
    public AudioClip[] footStepSounds;
    
    void Start()
    {
        
    }
    public void printMe(string s)
    {
        int randomIndex = Random.Range(0, footStepSounds.Length);
        SoundManager.PlayOneShot(footStepSounds[randomIndex]);
        //Debug.Log("PrintEvent: " + s + " called at: " + Time.time);
    }
}
