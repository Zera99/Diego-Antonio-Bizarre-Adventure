using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : IController
{
    PlayerModel _model;

    public IController SetModel(PlayerModel pl)
    {
        _model = pl;

        return this;
    }

    public void ListenKeys()
    {
        
        _model.MoveHorizontal(Input.GetAxisRaw("Horizontal"));

        _model.MoveVertical(Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _model.GroundedJump();
        }
    }


    public void ListenFixedKeys()
    {
        

    }
}
