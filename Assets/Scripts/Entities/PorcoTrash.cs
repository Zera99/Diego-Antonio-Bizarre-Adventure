using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcoTrash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerModel p = collision.gameObject.GetComponent<PlayerModel>();
        if (p != null)
            p.TakeDamage(1);
    }

}
