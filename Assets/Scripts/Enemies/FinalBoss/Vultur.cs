using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vultur : MonoBehaviour {

    Animator anim;
    bool isDying;
    private void Awake() {
        anim = GetComponent<Animator>();
        isDying = false;
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        Egg e = collision.gameObject.GetComponent<Egg>();
        if (e != null && !isDying) {
            isDying = true;
            anim.SetTrigger("Egg");
            EndGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        BowserFire f = collision.gameObject.GetComponent<BowserFire>();
        if (f != null && !isDying) {
            isDying = true;
            anim.SetTrigger("Fire");
            EndGame();
        }
    }

    void EndGame() {
        StartCoroutine(WaitToEndScene());
    }

    IEnumerator WaitToEndScene() {
        yield return new WaitForSeconds(6.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
