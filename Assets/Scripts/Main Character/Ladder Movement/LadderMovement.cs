using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LadderMovement : IMovement
{
    Transform _xTrf;
    Action _movingUp;
    Action _movingDown;
    Action<float> _onYMovement;

    public LadderMovement(Transform xTrf, Action onMoveUp, Action onMoveDown, Action<float> viewAction)
    {
        _xTrf = xTrf;
        _movingUp = onMoveUp;
        _movingDown = onMoveDown;
        _onYMovement = viewAction;
    }

    public void MoveHorizontal(float xAxi, float speed, bool isRunning, Action extraActions)
    {
        extraActions();
        _xTrf.position += new Vector3(xAxi * speed * Time.deltaTime, 0);
    }

    public void MoveVertical(float yAxi, float speed)
    {
        if (yAxi != 0)
        {
            if (yAxi > 0)
            {
                _movingUp();
            }
            else if (yAxi < 0)
            {
                _movingDown();
            }

            _xTrf.position += new Vector3(0, yAxi * speed * Time.deltaTime);
        } 

        _onYMovement(yAxi);
    }
}