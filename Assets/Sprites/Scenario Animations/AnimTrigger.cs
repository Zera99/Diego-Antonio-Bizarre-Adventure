using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    Animator anim;
    public AudioSource screen;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        anim.SetTrigger("playAnim");
        screen.Play();
    }
}
