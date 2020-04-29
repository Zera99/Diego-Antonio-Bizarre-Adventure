using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GroundMovement : IMovement
{
    Transform _xTrf;
    Action<float> _onMove;
    Func<bool> _wallDetect;

    public GroundMovement(Transform xTrf, Action<float> onMove, Func<bool> wallCollisionCheck)
    {
        _xTrf = xTrf;
        _onMove = onMove;
        _wallDetect = wallCollisionCheck;
    }
    

    public void MoveHorizontal(float xAxi, float speed, bool isRunning, Action extraActions)
    {
        extraActions();

        if (!_wallDetect() && xAxi!=0)
        {
            _xTrf.position += new Vector3(xAxi * speed * Time.deltaTime, 0);

            float moveAxiX = Mathf.Abs((isRunning) ? xAxi * 2 : xAxi);

            _onMove(moveAxiX);
        }
        else
            _onMove(0);
    }

    public void MoveVertical(float yAxi, float speed)
    {

    }
}
