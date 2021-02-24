using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawPile : MonoBehaviour {

    public VisionAreas vision;
    public int Damage;
    public float range;
    PlayerModel _player;
    Animator _anim;

    private void Awake() {
        _anim = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerModel>();
        vision = VisionAreas.NONE;
    }

    // Update is called once per frame
    void Update() {
        if(vision != VisionAreas.NONE && Vector3.Distance(this.gameObject.transform.position, _player.transform.position) <= range) {
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
                break;
        }
    }


}
