using System.Collections;
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
            boss.Attack();
            boss.ClawSound();
            p.TakeDamage(boss.ClawDamage);
        } 
    }



}
