using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkyComponent : MonoBehaviour {
    SpriteRenderer _renderer;
    public Sprite on;
    public Sprite off;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void InProximity(int attackTimer) {
        StartCoroutine(ActivateComponent(attackTimer));
    }

    IEnumerator ActivateComponent(int delayAmount) {
        for (int i = 0; i < delayAmount; i++) {
            if (i % 2 == 0)
                _renderer.sprite = on;
            else
                _renderer.sprite = off;
            yield return new WaitForSeconds(0.2f);
        }
        _renderer.sprite = on;
    }

    public void DeactivateComponent() {
        _renderer.sprite = off;
    }

}
