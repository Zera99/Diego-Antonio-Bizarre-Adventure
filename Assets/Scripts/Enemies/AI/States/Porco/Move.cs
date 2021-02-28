using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IState {

    float moveSpeed;
    Porco boss;
    Vector3 dir;

    public Move(Porco b, float m) {
        boss = b;
        moveSpeed = m;
        dir = new Vector3(1.0f, 0f, 0f);
    }

    public void Enter() {
        dir *= -1;
    }

    public void Exec() {
        boss.transform.position += dir.normalized * moveSpeed * Time.deltaTime;
    }

    public void Exit() {
        
    }

    public void SetMoveSpeed(float m) {
        moveSpeed = m;
    }

}
