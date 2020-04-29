using UnityEngine;

public class PhysicsCalculation {
    public float _jumpGravity;
    float _jumpVelocity;

    //transform.Translate(stepMovement);

    public void CalculateGravity(float jumpHeight, float timeToApex) {
        _jumpGravity = -(2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
    }

    public float CalculateJumpVelocity(float timeToApex) {
        _jumpVelocity = Mathf.Abs(_jumpGravity) * timeToApex;

        return _jumpVelocity;
    }

}
