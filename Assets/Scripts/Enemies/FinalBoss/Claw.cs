﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour {

    FinalBoss boss;
    bool flipped;

    private void Awake() {
        boss = transform.parent.GetComponent<FinalBoss>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerModel p = collision.gameObject.GetComponent<PlayerModel>();
        if(p != null) {
            boss.ClawAttack();
            p.TakeDamage(boss.ClawDamage);
        } else if (collision.gameObject.tag == "Wall" && !flipped) {
            Debug.Log("Collisioned with wall");
            boss.FlipDir();
            flipped = true;
            StartCoroutine(ResetFlip());
        }
    }

    IEnumerator ResetFlip() {
        yield return new WaitForSeconds(1.5f);
        flipped = false;
    }
}
