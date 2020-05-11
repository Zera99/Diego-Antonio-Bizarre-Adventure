using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour, IHazard {

    public float speed;
    public int damage;

    float timeToCrush;
    Vector3 originalPosition;
    public bool crushing;
    bool isWaiting;

    public AudioSource audioSource;
    public AudioClip crushingSound;

    // Start is called before the first frame update
    void Start() {
        timeToCrush = Random.Range(2, 3);
        originalPosition = this.transform.position;
        crushing = false;
    }

    // Update is called once per frame
    void Update() {
        if (crushing) {
            transform.position -= transform.up * speed * Time.deltaTime;
            return;
        } else if (Vector2.Distance(originalPosition, this.transform.position) >= 0.2f) {
            transform.position += transform.up * (speed / 3) * Time.deltaTime;
            return;
        } else if (!isWaiting) {
            StartCoroutine(CrushTime());
        }
    }

    IEnumerator CrushTime() {
        isWaiting = true;
        yield return new WaitForSeconds(timeToCrush);
        StartCoroutine(AboutToCrush());
    }

    IEnumerator AboutToCrush() {
        for (int i = 0; i < 8; i++) {
            yield return new WaitForSeconds(0.1f);
            if (i % 2 == 0) {
                transform.position += new Vector3(0, 0.2f);
            } else {
                transform.position -= new Vector3(0, 0.2f);
            }

        }

        crushing = true;

    }

    //private void OnTriggerEnter2D(Collision2D collision) {

    //if (collision.gameObject.GetComponent<PlayerModel>() != null && crushing) {
    //    collision.gameObject.GetComponent<PlayerModel>().TakeDamage(damage);

    //    crushing = false;
    //    transform.position += new Vector3(0, 0.1f);
    //    isWaiting = false;
    //} else if(crushing)
    //{
    //    crushing = false;
    //    transform.position += new Vector3(0, 0.1f);
    //    isWaiting = false;
    //}
    //audioSource.PlayOneShot(crushingSound);
    //}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (crushing) {
            if (collision.gameObject.GetComponent<PlayerModel>() != null) {
                Debug.Log("Triggered on player");
                MakeDamage(collision.gameObject.GetComponent<PlayerModel>());
            }
            crushing = false;
            transform.position += new Vector3(0, 0.1f);
            isWaiting = false;
        }

        audioSource.PlayOneShot(crushingSound);
    }

    public void MakeDamage(PlayerModel player) {
        player.TakeDamage(damage);
    }
}
