using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlatform : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.GetComponent<RatBoi>() != null) {
            collision.gameObject.GetComponent<RatBoi>().Land();
        }
    }

}
