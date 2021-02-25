using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchFork : MonoBehaviour {
    public int Damage;
    Porco boss;
    Vector3 myPos;

    private void Awake() {
        boss = transform.parent.GetComponent<Porco>();
        myPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerModel p = collision.gameObject.GetComponent<PlayerModel>();
        if (p != null) {
            p.TakeDamage(Damage);
        } else if (collision.gameObject.tag == "Wall") {
            boss.GetStuck();
        }
    }
}
