using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IState {

    float timeIdle;
    float internalTime;
    Porco boss;

    public Idle(Porco b, float t) {
        boss = b;
        timeIdle = t;
    }


    public void Enter() {
        internalTime = 0;
    }

    public void Exec() {
        internalTime += Time.deltaTime;
        if (internalTime > timeIdle) {
            boss.Move(true);
        }
    }

    public void Exit() {
        throw new System.NotImplementedException();
    }
}
