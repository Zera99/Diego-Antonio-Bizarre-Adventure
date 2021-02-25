using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : IState {

    float jumpForce;
    Vector2 jumpDir;
    float _internalTime;
    Porco boss;
    Rigidbody2D rb;
    int jumpCount;
    int maxJumps;

    public Jump(Porco b, float J, Rigidbody2D r) {
        boss = b;
        jumpForce = J;
        rb = r;
        jumpDir = new Vector2(0.0f, 1.0f);
    }

    public void Enter() {
        _internalTime = 1.6f;
    }

    public void Exec() {
        _internalTime += Time.deltaTime;
        if (_internalTime >= 1.5f) {
            rb.AddForce(jumpDir.normalized * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            if (jumpCount > maxJumps) {
                boss.Move(false);
            }
            _internalTime = 0;
        }
    }

    public void Exit() {
        jumpCount = 0;
    }

    public void SetDir(Vector2 dir) {
        jumpDir = dir;
    }

    public void SetMaxJumps(int jumps) {
        maxJumps = jumps;
    }
}
