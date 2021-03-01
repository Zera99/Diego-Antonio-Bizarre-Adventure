using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneFromStats : MonoBehaviour
{

    public LatinLoverStats stats;
    public void LoadSceneOnStats() {
        SceneManager.LoadScene(stats.CurrentSceneIndex);
    }

}
