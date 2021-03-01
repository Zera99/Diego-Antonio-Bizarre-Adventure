using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2 : PhaseBase {

    FinalBossMoveMissile moveMissileState;
    FinalBossHurt hurtState;

    private void Start() {
        Transform player = GameObject.FindObjectOfType<PlayerModel>().transform;
        moveMissileState = new FinalBossMoveMissile(this.boss,this.boss.MissileMoveSpeed, this.boss.FastMissilePrefab, player);
        hurtState = new FinalBossHurt(this.boss);


        fsm = new FSM();
        fsm.ChangeState(moveMissileState);
    }

    public override void OnUpdate() {
        fsm.Update();
    }

    public override void ChangeDir() {
        moveMissileState.FlipDirection();
    }

    public override void FinishHurt() {
        fsm.ChangeState(moveMissileState);
    }

    public override void GetHurt() {
        fsm.ChangeState(hurtState);
    }
}
