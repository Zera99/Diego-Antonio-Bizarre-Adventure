using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public GameObject Retry;
    public GameObject Menu;

    public void EnableButtons() {
        Retry.SetActive(true);
        Menu.SetActive(true);
    }
}
