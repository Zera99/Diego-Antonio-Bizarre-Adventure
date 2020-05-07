using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackSkill : ISkill
{
    bool _isUsingJet;

    PlayerModel _pl;
    Transform _xTrf;
    float _speed;

    IController _jetpackKeys;
    IMovement _jetpackMovement;


    public JetpackSkill(PlayerModel pl, float speed)
    {
        _isUsingJet = false;

        _pl = pl;
        _xTrf = _pl.transform;
        _speed = speed;

        _jetpackKeys = new JetpackController(_pl);
        _jetpackMovement = new JetpackMovement(_pl.transform);
    }

    public void PrepareSkill()
    {
        if (!_isUsingJet)
        {
            _isUsingJet = true;
            _pl.SetNewStrategies(_jetpackKeys, _jetpackMovement); //Seteo el movimiento y keys del jetpack

            //Parar la fisica de gravedad.
            //Setear al action de eliminar cosas al ser golpeado esto.
        }
        else
        {
            _isUsingJet = false;
            _pl.RestorePhysics();  //Me fijo si estoy en el aire o ground y seteo las keys y movement
        }
    }

    public void SecondSkill()
    {
        if (_isUsingJet)
        {

            Debug.Log("I BELIEVE I CAN FLY");
            _xTrf.position += new Vector3(0, _speed * Time.deltaTime);
        }
    }

    public void UseSkill()
    {
        
    }
}
