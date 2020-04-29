using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamerView : MonoBehaviour
{
    Animator _anim;
    SpriteRenderer _renderer;
    Color _oldColor;

    private void Start() {
        _anim = this.GetComponent<Animator>();
        _renderer = this.GetComponent<SpriteRenderer>();
        _oldColor = _renderer.color;
    }

    public void SpriteFlip(bool flip) {
        _renderer.flipX = flip;
    }

    public void TakeDamage() {
        _anim.Play("Damage Animation");
        StartCoroutine(BlinkEffect());
    }

    public void Die() {
        _anim.Play("Die Animation");
    }

    public void Attack() {
        _anim.Play("Attack Animation");
    }

    IEnumerator BlinkEffect()
    {
        _renderer.color = new Color(255, _oldColor.g/2, _oldColor.b/2, 0.8f);
        for (int i = 0; i < 5; i++)
        {
            _renderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            _renderer.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
        _renderer.color = _oldColor;
    }

}
