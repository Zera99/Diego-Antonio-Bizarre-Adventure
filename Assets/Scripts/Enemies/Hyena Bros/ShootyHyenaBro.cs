using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyHyenaBro : MonoBehaviour {
    FSM _fsm;
    MoveAndShootState       _moveState;
    MoveWithoutShieldState  _moveVulnerableState;

    public float timeBetweenStateChange;
    float _currentTime;

    public Transform wp1, wp2;
    public float moveSpeed;
    public float vulnerableMoveSpeed;

    public GameObject hyenaBulletPrefab;
    public Transform bulletSpawnPoint;
    public float shootingCooldown;
    // Start is called before the first frame update
    void Start() {
        _fsm = new FSM();
        _moveState = new MoveAndShootState(this, wp1, wp2, hyenaBulletPrefab).SetParameters(shootingCooldown, moveSpeed);
        _moveVulnerableState = new MoveWithoutShieldState(this, vulnerableMoveSpeed, wp1, wp2);
        _fsm.ChangeState(_moveState);
    }

    // Update is called once per frame
    void Update() {
        _fsm.Update();
        _currentTime += Time.deltaTime;

        if(_currentTime >= timeBetweenStateChange) {
            if(_fsm.PeekCurrentState() == _moveState) {
                _fsm.ChangeState(_moveVulnerableState);
            } else {
                _fsm.ChangeState(_moveState);
            }
            _currentTime = 0;
        }
    }

    public void Shoot() {
        GameObject bullet = Instantiate(hyenaBulletPrefab);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.GetComponent<ShellBullet>().Direction = Vector3.left;
    }
}
