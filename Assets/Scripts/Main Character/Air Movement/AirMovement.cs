using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AirMovement : IMovement
{
    Transform _xTrf;
    Func<bool> _wallDetect;

    public AirMovement(Transform xTrf, Func<bool> wallCollisionCheck)
    {
        _xTrf = xTrf;
        _wallDetect = wallCollisionCheck;
    }
    
    public void MoveHorizontal(float xAxi, float speed, bool isRunning, Action extraActions)
    {
        extraActions();

        if (!_wallDetect() && xAxi != 0)
        {
            _xTrf.position += new Vector3(xAxi * speed * Time.deltaTime, 0);
        }
    }

    public void MoveVertical(float yAxi, float speed)
    {

    }
}
