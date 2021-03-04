using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public Image loadingBarFill;
    public LatinLoverStats stats;

    public void LoadLevel(int sceneIndex)
    {
        stats.hp = stats.maxHP;
        stats.lives = stats.maxLives;
        StartCoroutine(LoadLevelAsync(sceneIndex));
    }

    IEnumerator LoadLevelAsync(int sceneIndex)
    {
        loadingBarFill.fillAmount = 0;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {

            loadingBarFill.fillAmount = asyncLoad.progress;
            yield return null;
        }

        loadingBarFill.fillAmount = 1;

        yield return new WaitForSeconds(1);

        asyncLoad.allowSceneActivation = true;
    }
}
