using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    Collider2D fireCollider;
    public int damage;

    public AudioSource audioSource;
    public AudioClip fireSound;

    private void Start() {
        fireCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>())
            collision.gameObject.GetComponent<PlayerModel>().TakeDamage(damage);
    }

    public void EnableFire() {
        audioSource.PlayOneShot(fireSound);
        fireCollider.enabled = true;
    }

    public void DisableFire() {
        fireCollider.enabled = false;
    }

}
