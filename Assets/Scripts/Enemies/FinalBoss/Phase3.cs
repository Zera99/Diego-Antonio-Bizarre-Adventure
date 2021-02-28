using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3 : PhaseBase {

    FinalBossMoveElectric moveElectricState;
    FinalBossHurt hurtState;

    private void Start() {
        moveElectricState = new FinalBossMoveElectric(this.boss, this.boss.ElectricMoveSpeed);
        hurtState = new FinalBossHurt(this.boss);

        fsm = new FSM();
        fsm.ChangeState(moveElectricState);
    }

    public override void OnUpdate() {
        Debug.Log("FSM is: " + fsm);
        Debug.Log("Current State: " + fsm.PeekCurrentState());
        fsm.Update();
    }

    public override void ChangeDir() {
        moveElectricState.FlipDirection();
    }

    public override void FinishHurt() {
        fsm.ChangeState(moveElectricState);
    }

    public override void GetHurt() {
        fsm.ChangeState(hurtState);
    }
}
