using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public Image progressBar;

    void Start()
    {
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("GameScene");

        operation.allowSceneActivation = false;
        yield return new WaitForSeconds(3f);
        operation.allowSceneActivation = true;

        while (operation.isDone == false)
        {
            progressBar.fillAmount = operation.progress;

            if (progressBar.fillAmount >= 0.9f)
                progressBar.fillAmount = 1.0f;

            yield return new WaitForEndOfFrame();
        }
    }
}
