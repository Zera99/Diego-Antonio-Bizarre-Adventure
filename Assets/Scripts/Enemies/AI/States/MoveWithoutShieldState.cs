using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithoutShieldState : IState {
    ShootyHyenaBro _hyena;
    Transform _waypoint1;
    Transform _waypoint2;
    Transform _targetWP;
    Vector3 _currentDir;
    float _speed;

    public MoveWithoutShieldState(ShootyHyenaBro hyena, float speed, Transform wp1, Transform wp2) {
        _hyena = hyena;
        _waypoint1 = wp1;
        _waypoint2 = wp2;
        _speed = speed;
    }

    public void Enter() {
        _targetWP = _waypoint1;
        _currentDir = (_waypoint1.position - _hyena.transform.position).normalized;
        // Feedback Cambio de estado
        // Activar Collider para permitir los hits.
    }

    public void Exec() {
        Move();
    }

    public void Exit() {

    }

    void Move() {
        _hyena.transform.position += _currentDir * _speed * Time.deltaTime;
        if (Vector3.Distance(_hyena.transform.position, _targetWP.position) < 0.2f) {
            SwitchTarget();
            _currentDir = CalculateDirection();
        }
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
