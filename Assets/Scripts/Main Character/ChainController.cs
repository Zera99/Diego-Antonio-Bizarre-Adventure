using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainController : IController
{
    PlayerModel _model;

    public IController SetModel(PlayerModel pl)
    {
        _model = pl;

        return this;
    }

    public void ListenKeys()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _model.GroundedJump();
        }
    }

    public void ListenFixedKeys()
    {

    }

}
