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
        BowserFire f = collision.gameObject.GetComponent<BowserFire>();

        if(e != null || f != null) {
            boss.TakeDamage(1);
        }
    }
}
