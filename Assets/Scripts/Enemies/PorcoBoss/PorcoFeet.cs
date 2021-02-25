using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcoFeet : MonoBehaviour {

    Porco b;
    private void Awake() {
        b = transform.parent.GetComponent<Porco>();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall") {
            b.HasLanded();
        }
    }
}
