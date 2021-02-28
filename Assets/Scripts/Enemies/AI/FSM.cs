using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM {

    IState _currentState;

    public void ChangeState(IState newState) {
        if (_currentState != null) {
            _currentState.Exit();
        }
        Debug.Log("New State: " + newState.GetType().ToString());
        _currentState = newState;
        _currentState.Enter();
    }

    public void Update() {
        if (_currentState != null)
            _currentState.Exec();
    }

    public IState PeekCurrentState() {
        return _currentState;
    }

}
