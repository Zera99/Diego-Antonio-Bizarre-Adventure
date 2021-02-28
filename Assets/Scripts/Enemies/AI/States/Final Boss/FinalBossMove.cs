using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossMove : IState {
    float moveSpeed;
    FinalBoss boss;
    Vector3 dir;

    public FinalBossMove(FinalBoss b, float m) {
        boss = b;
        moveSpeed = m;
        dir = new Vector3(-1.0f, 0f, 0f);
    }

    public void Enter() {

    }

    public void Exec() {
        boss.transform.position += dir.normalized * moveSpeed * Time.deltaTime;
    }

    public void Exit() {

    }

    public void FlipDirection() {

        dir *= -1;
    }
}
