using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlatform : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.GetComponent<PlayerModel>()) {
            //collision.gameObject.GetComponent<PlayerModel>().ResetJumps();
            //collision.gameObject.GetComponent<PlayerModel>().Land();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //if (collision.gameObject.GetComponent<PlayerModel>()) {
        //    collision.gameObject.GetComponent<PlayerModel>()._isInAir = true;
        //}
    }

}
