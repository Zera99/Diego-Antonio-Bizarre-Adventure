using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerModel p = collision.gameObject.GetComponent<PlayerModel>();
        if (p != null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
