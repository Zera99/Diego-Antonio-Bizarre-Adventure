using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrusherPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.GetComponent<PlayerModel>() != null) {
            collision.gameObject.GetComponent<PlayerModel>().SetOnCrusher(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<PlayerModel>() != null) {
            collision.gameObject.GetComponent<PlayerModel>().SetOnCrusher(false);
        }
    }
}
