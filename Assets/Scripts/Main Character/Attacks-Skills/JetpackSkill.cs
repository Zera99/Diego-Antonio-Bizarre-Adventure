using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackSkill : ISkill
{
    bool _isUsingJet;

    PlayerModel _pl;
    Transform _xTrf;
    JetpackStatsSO _stats;
    Skin _thisSkin;
    PlayerView view;

    IController _jetpackKeys;
    IMovement _jetpackMovement;

    Rigidbody2D _rgbd;

    Coroutine _rechargingJetCoroutine, _usingJetCoroutine;

    System.Action _startRecharge;
    System.Action _looseJetPoints;

    public JetpackSkill(PlayerModel pl, JetpackStatsSO stats, Skin thisSkin)
    {
        _isUsingJet = false;

        _pl = pl;
        _xTrf = _pl.transform;
        _stats = stats;
        _rgbd = pl.GetComponent<Rigidbody2D>();
        _jetpackKeys = new JetpackController(_pl);
        _jetpackMovement = new JetpackMovement(_pl.transform);
        _thisSkin = thisSkin;

        view = pl.gameObject.GetComponent<PlayerView>();
    }

    void ReleaseJetpack()
    {
        _isUsingJet = false;
        _pl.RestorePhysics();  //Le restauro el uso de fisica
        _pl.CancelAttackActionsOnDmg(null);

        _startRecharge?.Invoke();
        _looseJetPoints?.Invoke();
    }

    public void PrepareSkill(PlayerModel pl, System.Action execute)
    {
        if (!_isUsingJet)
        {
            view.PlayJetpackSound();
            //PARO RECHARGE
            if (_rechargingJetCoroutine != null)
            {
                pl.StopRecharge(_rechargingJetCoroutine);
            }

            _isUsingJet = true;
            _pl.SetNewStrategies(_jetpackKeys, _jetpackMovement); //Seteo el movimiento y keys del jetpack

            _pl.ControlPhysics();//Parar la fisica de gravedad.
            //Setear al action de eliminar cosas al ser golpeado esto.
            _pl.CancelAttackActionsOnDmg(ReleaseJetpack);
            
            //LOOSE GAS
            _usingJetCoroutine = pl.RechargeBar(() => { return (pl.currentPointsJetPack > 0); }, () => { pl.currentPointsJetPack -= _stats.decayPointsPerSecond * Time.deltaTime; pl.ChangePointsValue(_thisSkin, Mathf.RoundToInt(pl.currentPointsJetPack)); }, () => { pl.currentPointsJetPack = 0; pl.ChangePointsValue(_thisSkin, 0); ReleaseJetpack(); });
            _looseJetPoints = () => pl.StopRecharge(_usingJetCoroutine);

            //RECHARGE
            _startRecharge = () => _rechargingJetCoroutine = pl.RechargeBar(() => { return (pl.currentPointsJetPack < _stats.maxPoints); }, () => { pl.currentPointsJetPack += _stats.rechargePointsPerSecond * Time.deltaTime; pl.ChangePointsValue(_thisSkin, Mathf.RoundToInt(pl.currentPointsJetPack)); }, () => { pl.currentPointsJetPack = _stats.maxPoints; pl.ChangePointsValue(_thisSkin, _stats.maxPoints); });
        }
        else
        {
            //_pl.CancelAttackActionsOnDmg(null);
            ReleaseJetpack();
        }

        execute();
    }

    public void SecondSkill()
    {
        if (_rgbd.velocity.y < _stats.topSpeed)
            _rgbd.AddForce(Vector2.up * _stats.upSpeed, ForceMode2D.Impulse);
    }

    public void UseSkill()
    {
        
    }

}
