using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public GameObject LoadingPanel;
    public Slider slider;
    public TextMeshProUGUI ProgressText;

    public static bool LoadingSavedFile = false;

    public void LoadLevel(int sceneIndex)
    {
        if(sceneIndex == 2)
            LoadingSavedFile = false;
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        LoadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            ProgressText.text = (int)(progress * 100) + "%";

            yield return null;
        }
    }

    public void LoadSaveLevel(int sceneIndex)
    {
        if(sceneIndex == 2)
            LoadingSavedFile = true;
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
}
