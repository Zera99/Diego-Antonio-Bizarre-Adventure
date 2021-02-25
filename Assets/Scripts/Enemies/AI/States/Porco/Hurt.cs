using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : IState {

    float timeHurt;
    float internalTime;
    Porco boss;

    public Hurt(Porco b, float T) {
        boss = b;
        timeHurt = T;
    }
    public void Enter() {

    }

    public void Exec() {
        internalTime += Time.deltaTime;
        if (internalTime >= timeHurt)
            boss.Unstuck();
    }

    public void Exit() {
        internalTime = 0;
    }
}
