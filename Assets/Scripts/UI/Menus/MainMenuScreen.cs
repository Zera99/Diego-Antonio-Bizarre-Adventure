using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    public void GoToScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void BtnQuit()
    {
        Application.Quit();
    }
}
