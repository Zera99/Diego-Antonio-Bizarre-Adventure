using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3 : PhaseBase {

    FinalBossMoveElectric moveElectricState;
    FinalBossHurt hurtState;

    private void Start() {
        
    }

    public override void OnUpdate() {
        // Movimiento, rayos y piso electrificado
    }

    public override void ChangeDir() {
        //FinalBossMoveMissile.FlipDirection();
    }

    public override void FinishHurt() {
        //fsm.ChangeState(moveElectricState);
    }

    public override void GetHurt() {
        fsm.ChangeState(hurtState);
    }
}
