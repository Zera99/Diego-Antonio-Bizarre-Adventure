﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricArea : MonoBehaviour {

    public BoxCollider2D myCollider;
    bool activated;
    Animator anim;
    AudioSource Source;
    public AudioClip electricity;

    private void Awake() {
        myCollider = GetComponent<BoxCollider2D>();
        Source = GetComponent<AudioSource>();
        activated = false;
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerModel p = collision.gameObject.GetComponent<PlayerModel>();
        if (p != null && activated)
            p.TakeDamage(1);
    }

    public void Activate() {
        activated = true;
        myCollider.enabled = true;
        StartCoroutine(WaitAndDeactivate());
        anim.SetBool("Active", true);
        Source.PlayOneShot(electricity);
    }

    public void Deactivate() {
        activated = false;
        myCollider.enabled = false;
        anim.SetBool("Active", false);
    }

    IEnumerator WaitAndDeactivate() {
        yield return new WaitForSeconds(0.5f);
        Deactivate();
    }
}
