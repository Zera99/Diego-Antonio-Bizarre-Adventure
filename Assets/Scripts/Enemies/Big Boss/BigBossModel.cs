using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossModel : MonoBehaviour {
    public int damage;

    BigBossView _view;
    bool _isStunned;
    bool _canAttack;

    // Start is called before the first frame update
    void Start() {
        _view = GetComponent<BigBossView>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Egg e = collision.GetComponent<Egg>();

        if(e && !_isStunned) {
            _canAttack = false;
            _isStunned = true;
            StartCoroutine(Stun());
            _view.SetAttacking(false);
            _view.Stun();
            return;
        }

        PlayerModel p = collision.GetComponent<PlayerModel>();
        if (p && !_isStunned) {
            _canAttack = true;
            _view.SetAttacking(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        PlayerModel p = collision.GetComponent<PlayerModel>();

        if (p && _canAttack) {
            p.TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        EndAttack();
    }

    public void EndAttack() {
        _view.SetAttacking(false);
    }

    IEnumerator Stun() {
        yield return new WaitForSeconds(2.0f);
        _isStunned = false;
    }
}
