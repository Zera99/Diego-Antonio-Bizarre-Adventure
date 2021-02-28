using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossWeakSpot : MonoBehaviour {

    FinalBoss boss;

    private void Awake() {
        boss = transform.parent.GetComponent<FinalBoss>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Egg e = collision.gameObject.GetComponent<Egg>();
        if (e != null) {
            boss.TakeDamage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        BowserFire f = collision.gameObject.GetComponent<BowserFire>();

        if (f != null) {
            boss.TakeDamage(1);
            Destroy(f.gameObject);
        }
    }
}
