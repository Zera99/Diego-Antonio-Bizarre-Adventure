using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AttackAnimExecution : StateMachineBehaviour
{
    protected Action _attack = delegate { };

    public void SetAttackAction(Action act)
    {
        _attack = act;
    }
}
