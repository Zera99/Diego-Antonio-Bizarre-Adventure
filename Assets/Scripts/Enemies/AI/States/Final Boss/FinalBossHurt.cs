using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossHurt : IState {

    FinalBoss boss;
    float internalTime;


    public FinalBossHurt (FinalBoss b) {
        boss = b;
    }

    public void Enter() {
        internalTime = 0;
    }

    public void Exec() {
        internalTime += Time.deltaTime;
        if(internalTime >= boss.WaitTimeOnHurt) {
            boss.FinishHurt();
        }

    }

    public void Exit() {

    }
}
