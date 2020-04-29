using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class EnableControlAnimation : DisableControlAnimation
{
    protected Action _enableControl = delegate { };

    public void SetEnableAction(Action act)
    {
        _enableControl = act;
    }
}
