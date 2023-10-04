using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public Image progressBar;
    private int _finishedGame;

    void Start()
    {
        _finishedGame = PlayerPrefs.GetInt("finishedGame");
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        AsyncOperation operation;

        yield return LocalizationSettings.InitializationOperation;

        if (_finishedGame == 0)
        {
            operation = SceneManager.LoadSceneAsync("TutorialScene");
        }
        else
        {
            operation = SceneManager.LoadSceneAsync("GameScene");
        }

        operation.allowSceneActivation = true; // Yükleme iþlemini baþlat

        while (!operation.isDone)
        {
            progressBar.fillAmount = operation.progress;

            if (progressBar.fillAmount >= 0.9f)
                progressBar.fillAmount = 1.0f;

            yield return new WaitForEndOfFrame();
        }
    }
}
