using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScene : MonoBehaviour
{
    public int sceneIndex;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<PlayerModel>())
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
}
