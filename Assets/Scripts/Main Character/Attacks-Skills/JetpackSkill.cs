using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackSkill : ISkill
{
    bool _isUsingJet;

    PlayerModel _pl;
    Transform _xTrf;
    float _upImpulse, _topSpeed;

    IController _jetpackKeys;
    IMovement _jetpackMovement;

    Rigidbody2D _rgbd;

    public JetpackSkill(PlayerModel pl, float upImpulse, float topSpeed)
    {
        _isUsingJet = false;

        _pl = pl;
        _xTrf = _pl.transform;
        _upImpulse = upImpulse;
        _topSpeed = topSpeed;
        _rgbd = pl.GetComponent<Rigidbody2D>();
        _jetpackKeys = new JetpackController(_pl);
        _jetpackMovement = new JetpackMovement(_pl.transform);
    }

    void ReleaseJetpack()
    {
        _isUsingJet = false;
        _pl.RestorePhysics();  //Le restauro el uso de fisica
    }

    public void PrepareSkill()
    {
        if (!_isUsingJet)
        {
            _isUsingJet = true;
            _pl.SetNewStrategies(_jetpackKeys, _jetpackMovement); //Seteo el movimiento y keys del jetpack

            _pl.ControlPhysics();//Parar la fisica de gravedad.
            //Setear al action de eliminar cosas al ser golpeado esto.
            _pl.CancelAttackActionsOnDmg(ReleaseJetpack);
        }
        else
        {
            _pl.CancelAttackActionsOnDmg(null);
            ReleaseJetpack();
        }
    }

    public void SecondSkill()
    {
        if (_rgbd.velocity.y < _topSpeed)
            _rgbd.AddForce(Vector2.up * _upImpulse, ForceMode2D.Impulse);
    }

    public void UseSkill()
    {
        
    }
}
