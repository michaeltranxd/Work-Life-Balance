using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceSoundManager : MonoBehaviour
{
    public AudioSource SoundManager;
    public AudioClip buttonHoverSound;
    public AudioClip buttonClickSound;

    public void onButtonHover()
    {
        SoundManager.PlayOneShot(buttonHoverSound, .3f);
    }
    public void onButtonClick()
    {
        SoundManager.PlayOneShot(buttonClickSound, .5f);
    }
}
