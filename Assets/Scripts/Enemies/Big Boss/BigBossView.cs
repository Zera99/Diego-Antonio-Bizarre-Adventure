using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossView : MonoBehaviour {

    Animator _anim;
    SpriteRenderer _renderer;
    //Color _oldColor;

    private void Start() {
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        //_oldColor = _renderer.color;
    }

    public void SetAttacking(bool state) {
        _anim.SetBool("isAttacking", state);
    }

    public void Stun() {
        _anim.SetTrigger("isStunned");
        //StartCoroutine(StunCoroutine());
    }

    //IEnumerator StunCoroutine() {
    //    _renderer.color = new Color(175, 175, 175, 0.7f);
    //    for (int i = 0; i < 8; i++) {
    //        _renderer.enabled = false;
    //        yield return new WaitForSeconds(0.1f);
    //        _renderer.enabled = true;
    //        yield return new WaitForSeconds(0.05f);
    //    }
    //    _renderer.color = _oldColor;
    //}
}
