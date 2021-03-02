using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawPile : MonoBehaviour {

    public VisionAreas vision;
    public int Damage;
    public float range;
    PlayerModel _player;
    Animator _anim;
    bool isBlinded;
    bool onCD;

    private void Awake() {
        _anim = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerModel>();
        vision = VisionAreas.NONE;
        isBlinded = false;
        onCD = false;
    }

    // Update is called once per frame
    void Update() {
        if(!isBlinded && vision != VisionAreas.NONE && Vector2.Distance(this.gameObject.transform.position, _player.transform.position) <= range && !onCD) {
            _player.TakeDamage(Damage);
        }
    }

    public void ChangeVision(VisionAreas v) {
        vision = v;
        switch(vision) {
            case VisionAreas.LEFT:
                _anim.ResetTrigger("StopAttacking");
                if(Vector3.Distance(this.gameObject.transform.position, _player.transform.position) <= range && !onCD) {
                    _anim.SetTrigger("AttackLeft");
                    StartCoroutine(AttackCD());
                }
                break;
            case VisionAreas.TOP:
                _anim.ResetTrigger("StopAttacking");
                if (Vector3.Distance(this.gameObject.transform.position, _player.transform.position) <= range && !onCD) {
                    _anim.SetTrigger("AttackTop");
                    StartCoroutine(AttackCD());
                }
                break;
            case VisionAreas.RIGHT:
                _anim.ResetTrigger("StopAttacking");
                if (Vector3.Distance(this.gameObject.transform.position, _player.transform.position) <= range && !onCD) {
                    _anim.SetTrigger("AttackRight");
                    StartCoroutine(AttackCD());
                }
                break;
            case VisionAreas.NONE:
                _anim.SetTrigger("StopAttacking");
                _anim.ResetTrigger("AttackRight");
                _anim.ResetTrigger("AttackLeft");
                _anim.ResetTrigger("AttackTop");
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Egg e = collision.gameObject.GetComponent<Egg>();
        if(e != null && _anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !isBlinded) {
            isBlinded = true;
            _anim.SetTrigger("Blinded");
            StartCoroutine(Blinded());
        }
    }

    IEnumerator AttackCD() {
        yield return new WaitForSeconds(0.7f);
        onCD = true;
        _anim.SetTrigger("StopAttacking");
        _anim.ResetTrigger("AttackRight");
        _anim.ResetTrigger("AttackLeft");
        _anim.ResetTrigger("AttackTop");
        yield return new WaitForSeconds(1.0f);
        onCD = false;
    }

    IEnumerator Blinded() {
        yield return new WaitForSeconds(2.0f);
        _anim.ResetTrigger("Blinded");
        isBlinded = false;
        _anim.SetTrigger("StopAttacking");
    }

}
