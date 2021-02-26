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

    private void Awake() {
        _anim = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerModel>();
        vision = VisionAreas.NONE;
        isBlinded = false;
    }

    // Update is called once per frame
    void Update() {
        if(!isBlinded && vision != VisionAreas.NONE && Vector3.Distance(this.gameObject.transform.position, _player.transform.position) <= range) {
            _player.TakeDamage(Damage);
        }
    }

    public void ChangeVision(VisionAreas v) {
        vision = v;
        switch(vision) {
            case VisionAreas.LEFT:
                _anim.ResetTrigger("StopAttacking");
                _anim.SetTrigger("AttackLeft");
                break;
            case VisionAreas.TOP:
                _anim.ResetTrigger("StopAttacking");
                _anim.SetTrigger("AttackTop");
                break;
            case VisionAreas.RIGHT:
                _anim.ResetTrigger("StopAttacking");
                _anim.SetTrigger("AttackRight");
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

    IEnumerator Blinded() {
        yield return new WaitForSeconds(2.0f);
        _anim.ResetTrigger("Blinded");
        isBlinded = false;
        _anim.SetTrigger("StopAttacking");
    }

}
