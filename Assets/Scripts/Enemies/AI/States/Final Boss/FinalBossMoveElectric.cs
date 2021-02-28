using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossMoveElectric : IState {
    float moveSpeed;
    FinalBoss boss;
    Vector3 dir;

    float internalTime;

    public FinalBossMoveElectric(FinalBoss b, float s) {
        boss = b;
        moveSpeed = s;
        dir = new Vector3(-1.0f, 0f, 0f);
    }


    public void Enter() {

    }

    public void Exec() {
        boss.transform.position += dir.normalized * moveSpeed * Time.deltaTime;
        internalTime += Time.deltaTime;
        if(internalTime >= boss.TimeUntilShock) {
            boss.PrepareElectricAttack();
            internalTime = 0.0f;
        }
    }

    public void Exit() {

    }

    public void FlipDirection() {
        dir *= -1;
    }
}
