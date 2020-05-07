using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : IController {
    PlayerModel _model;

    public IController SetModel(PlayerModel pl) {
        _model = pl;
        return this;
    }

    public void ListenKeys()
    {

        _model.MoveHorizontal(Input.GetAxisRaw("Horizontal"));

        _model.MoveVertical(Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space)) {
            _model.GroundedJump();
        }
        else if (Input.GetKeyDown(KeyCode.J)) {
            _model.Attack();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1)) {
            _model.ChangeSkin(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            _model.ChangeSkin(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _model.ChangeSkin(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _model.ChangeSkin(3);
        }
    }

    public void ListenFixedKeys() {


    }
}
