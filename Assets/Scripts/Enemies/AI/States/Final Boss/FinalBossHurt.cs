using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossHurt : IState {

    FinalBoss boss;
    float internalTime;
    float waitTimeOnHurt;

    public FinalBossHurt (FinalBoss b) {
        boss = b;
    }

    public void Enter() {
        internalTime = 0;
    }

    public void Exec() {
        internalTime += Time.deltaTime;
        if(internalTime >= waitTimeOnHurt) {
            boss.FinishHurt();
        }

    }

    public void Exit() {

    }

    IEnumerator WaitOnDamaged() {
        yield return new WaitForSeconds(3.0f);

    }


}
