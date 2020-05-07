using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JetpackMovement : IMovement
{
    Transform _xTrf;

    public JetpackMovement(Transform xTrf)
    {
        _xTrf = xTrf;
    }


    public void MoveHorizontal(float xAxi, float speed, bool isRunning, Action extraActions)
    {
        extraActions();

        _xTrf.position += new Vector3(xAxi * speed * Time.deltaTime, 0);
        
    }

    public void MoveVertical(float yAxi, float speed)
    {
    }
}