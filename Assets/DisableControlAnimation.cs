
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class DisableControlAnimation : StateMachineBehaviour
{
    protected Action _disableControl = delegate { };

    public void SetDisableAction(Action act)
    {
        _disableControl = act;
    }    
}
