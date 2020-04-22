using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Button LoadGameButton;
    public TextMeshProUGUI EnableLoadGameText;
    public TextMeshProUGUI DisableLoadGameText;

    public GameObject WarningPanel;
    public GameObject TutorialPanel;
    public LevelLoader levelLoader;

    public AudioSource audioSource;

    public AudioClip hoverSound;
    public AudioClip clickSound;
    public AudioClip popupSound;

    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void OnNewGame()
    {
        if (SaveSystem.SaveFileExists())
        {
            WarningPanel.SetActive(true);
        }
        else
        {
            TutorialPanel.SetActive(true);
        }
    }

    public void onNoWarning()
    {
        WarningPanel.SetActive(false);
    }

    public void onYesWarning()
    {
        WarningPanel.SetActive(false);
        TutorialPanel.SetActive(true);
    }

    public void onLoadGame()
    {
        levelLoader.LoadSaveLevel(2);
        WarningPanel.SetActive(false);
    }

    public void onYesTutorial()
    {
        levelLoader.LoadLevel(1);
        TutorialPanel.SetActive(false);
    }

    public void onNoTutorial()
    {
        levelLoader.LoadLevel(2);
        TutorialPanel.SetActive(false);
    }

    void Update()
    {
        if (SaveSystem.SaveFileExists())
        {
            LoadGameButton.interactable = true;
            EnableLoadGameText.gameObject.SetActive(true);
            DisableLoadGameText.gameObject.SetActive(false);
        }
        else
        {
            LoadGameButton.interactable = false;
            EnableLoadGameText.gameObject.SetActive(false);
            DisableLoadGameText.gameObject.SetActive(true);
        }
    }

    public void onButtonHoverSound()
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public void onButtonClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void onMenuPopup()
    {
        audioSource.PlayOneShot(popupSound);
    }
}
