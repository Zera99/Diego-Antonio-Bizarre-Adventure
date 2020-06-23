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

    bool _isDoneShooting;
    float _shotCooldown;
    float _breathingTime;
    int _round;
    int _roundsForRatBoi;

    public MoveAndShootState(ShootyHyenaBro hyena, Transform wp1, Transform wp2) {
        _hyena = hyena;
        _waypoint1 = wp1;
        _waypoint2 = wp2;
    }

    public MoveAndShootState SetParameters(float shotCD, float speed, float breathingTime, int roundsForRatBoi) {
        _shotCooldown = shotCD;
        _speed = speed;
        _breathingTime = breathingTime;
        _roundsForRatBoi = roundsForRatBoi;
        return this;
    }

    public void Enter() {
        _targetWP = _waypoint2;
        _currentDir = (_waypoint2.position - _hyena.transform.position).normalized;
        _isDoneShooting = true;
        // Feedback Cambio de estado
    }

    public void Exec() {
        Move();
        if(_isDoneShooting) {
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
        _isDoneShooting = false;
        _hyena.StartCoroutine(ShootSet()); // Horrible, lo se. 
    }

    IEnumerator ShootSet() {
        int amount = Random.Range(1, 6);
        int type = _round % 4;
        while(amount > 0) {
            switch(type) {
                case 0:
                case 1:
                    _hyena.ShootBottle();
                    break;
                case 2:
                    _hyena.ThrowBox();
                    break;
                case 3:
                    _hyena.ShootMissile();
                    break;
            }

            amount--;
            yield return new WaitForSeconds(_shotCooldown);
        }

        _round++;
        if(_round > _roundsForRatBoi) {
            _hyena.SpawnRatBoi();
        }
        yield return new WaitForSeconds(_breathingTime);
        _isDoneShooting = true;
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
