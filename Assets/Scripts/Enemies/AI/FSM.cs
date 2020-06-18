﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM {

    IState _currentState;

    public void ChangeState(IState newState) {
        if (_currentState != null) {
            _currentState.Exit();
        }

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