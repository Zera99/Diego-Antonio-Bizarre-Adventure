using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKillTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<RatBoi>() != null) {
            collision.GetComponent<RatBoi>().Die();
        }

        if(collision.GetComponent<FlammableBox>() != null) {
            collision.GetComponent<FlammableBox>().SetOnFire();
        }
    }
}
