using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackController : IController
{
    PlayerModel _model;

    public JetpackController(PlayerModel pl)
    {
        _model = pl;
    }

    public IController SetModel(PlayerModel pl)
    {
        _model = pl;

        return this;
    }

    public void ListenKeys()
    {

        _model.MoveHorizontal(Input.GetAxisRaw("Horizontal"));

        if (Input.GetKeyDown(KeyCode.J))
        {
            _model.Attack();
        }
        else if (Input.GetKey(KeyCode.K))
        {
            _model.SecondAttack();
        }
    }


    public void ListenFixedKeys()
    {
    }
}