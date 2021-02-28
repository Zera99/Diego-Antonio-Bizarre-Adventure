using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuck : IState {

    float timeStuck;
    float internalTime;
    BoxCollider2D vulnerableArea;
    Porco boss;

    public Stuck(Porco b, float T, BoxCollider2D V) {
        boss = b;
        timeStuck = T;
        vulnerableArea = V;
    }

    public void Enter() {
        vulnerableArea.enabled = true;
        internalTime = 0;
        boss.ToggleVulnerable();
    }

    public void Exec() {
        internalTime += Time.deltaTime;
        if(internalTime > timeStuck) {
            Debug.Log("True?");
            boss.Move(true);
        }
    }

    public void Exit() {
        Debug.Log("Exiting Stuck state");
        vulnerableArea.enabled = false;
        boss.ToggleVulnerable();
        boss.Unblind();

    }
}
