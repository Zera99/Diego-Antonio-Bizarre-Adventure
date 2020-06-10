using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateSprite : MonoBehaviour
{
    SpriteRenderer _renderer;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<PlayerModel>() != null)
            _renderer.enabled = false;
    }
}
