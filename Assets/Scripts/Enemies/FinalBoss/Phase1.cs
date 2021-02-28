using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1 : PhaseBase {

    FinalBossMove moveState;
    FinalBossHurt hurtState;


    void Start() {

        moveState = new FinalBossMove(this.boss, this.boss.MoveSpeed);
        hurtState = new FinalBossHurt(this.boss);

        fsm = new FSM();
        fsm.ChangeState(moveState);
    }

    public override void OnUpdate() {
        // Movimiento, trigger garra.
        fsm.Update();
    }

    public override void ChangeDir() {
        moveState.FlipDirection();
    }

    public override void FinishHurt() {
        fsm.ChangeState(moveState);
    }

    public override void GetHurt() {
        fsm.ChangeState(hurtState);
    }
}
