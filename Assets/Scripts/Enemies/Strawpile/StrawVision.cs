using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawVision : MonoBehaviour {
    StrawPile pile;
    public VisionAreas myZone;
    private void Awake() {
        pile = transform.parent.GetComponent<StrawPile>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerModel p = collision.gameObject.GetComponent<PlayerModel>();
        if (p != null) {
            pile.ChangeVision(myZone);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        PlayerModel p = other.gameObject.GetComponent<PlayerModel>();
        if (p != null) {
            pile.ChangeVision(VisionAreas.NONE);
        }
    }
}
