using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhaseBase : MonoBehaviour{

    protected FinalBoss boss;
    protected FSM fsm;

    protected void Awake() {
        boss = GetComponent<FinalBoss>();
    }

    public abstract void OnUpdate();
    public abstract void ChangeDir();

    public abstract void FinishHurt();

    public abstract void GetHurt();
}
