using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{

    private void Awake() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    public void GoToScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void BtnQuit()
    {
        Application.Quit();
    }
}
