using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseManager : MonoBehaviour
{
    public static bool GamePaused;
    public GameObject PausePanel;
    public GameObject SettingsPanel;
    public Player player;

    public AudioMixer audioMixer;

    private bool mouseShownBefore;

    // Update is called once per frame

    void Start()
    {
        GamePaused = false;
        mouseShownBefore = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused && !SettingsPanel.activeSelf)
            {
                onResume();
            }
            else if(!GamePaused && !SettingsPanel.activeSelf)
            {
                onPause();
            }
            else if(GamePaused && SettingsPanel.activeSelf)
            {
                goBackToPausedMenu();
            }
        }    
    }

    public void onResume()
    {
        GamePaused = false;
        PausePanel.SetActive(false);
        player.playerInControl = true;
        if(!mouseShownBefore)
            Player.hideMouse();
    }

    public void onPause()
    {
        GamePaused = true;
        PausePanel.SetActive(true);
        player.playerInControl = false;
        if (Player.cursorShown)
        {
            mouseShownBefore = true;
        }
        Player.showMouse();
    }

    public void onSettings()
    {
        SettingsPanel.SetActive(true);
        PausePanel.SetActive(false);
    }

    public void onButtonHover()
    {
        // TODO add sound effects
    }

    public void goBackToPausedMenu()
    {
        PausePanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("gameVolume", volume);
        Debug.Log("volume" + volume);
    }
}
