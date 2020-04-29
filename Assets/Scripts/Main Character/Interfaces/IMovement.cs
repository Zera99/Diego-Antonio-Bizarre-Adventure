using System;

public interface IMovement
{
    void MoveHorizontal(float xAxi, float speed, bool isRunning, Action extraActions);
    void MoveVertical(float yAxi, float speed);
}
