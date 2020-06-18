using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndShootState : IState {

    ShootyHyenaBro _hyena;
    Transform _waypoint1;
    Transform _waypoint2;
    Transform _targetWP;
    Vector3 _currentDir;
    float _speed;

    GameObject _bulletPrefab;
    float _timeToNextShot;
    float _shotCooldown;

    public MoveAndShootState(ShootyHyenaBro hyena, Transform wp1, Transform wp2, GameObject bulletPrefab) {
        _hyena = hyena;
        _waypoint1 = wp1;
        _waypoint2 = wp2;
        _bulletPrefab = bulletPrefab;
    }

    public MoveAndShootState SetParameters(float shotCD, float speed) {
        _shotCooldown = shotCD;
        _speed = speed;
        return this;
    }

    public void Enter() {
        _targetWP = _waypoint2;
        _currentDir = (_waypoint2.position - _hyena.transform.position).normalized;
        // Feedback Cambio de estado
    }

    public void Exec() {
        Move();
        _timeToNextShot += Time.deltaTime;
        if(_timeToNextShot >= _shotCooldown) {
            Shoot();
        }
    }

    public void Exit() {
        // Feedback Cambio de estado
    }

    void Move() {
        _hyena.transform.position += _currentDir * _speed * Time.deltaTime;
        if(Vector3.Distance(_hyena.transform.position, _targetWP.position) < 0.2f) {
            SwitchTarget();
            _currentDir = CalculateDirection();
        }
    }

    void Shoot() {
        _timeToNextShot = 0;
        _hyena.Shoot();
    }

    Vector3 CalculateDirection() {
        return (_targetWP.position - _hyena.transform.position).normalized;
    }

    void SwitchTarget() {
        if (_targetWP == _waypoint1)
            _targetWP = _waypoint2;
        else
            _targetWP = _waypoint1;
    }

}
