using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerController
{
    PlayerModel _model;
    IController _actualController;
    Action<PlayerModel> _skinKeys;

    public PlayerController(PlayerModel m, PlayerView v)
    {
        _model = m;

        v.SetDisableControlBehaviour(DisableControls);
        v.SetEnableControlBehaviour(EnableControls);

        m.onSkinKeys += SkinKeys;
        m.onUpdate += ControllerUpdate;
        m.onFixedUpdate += ControllerFixedUpdate;
        m.onChangeSkin += v.SetSkin;
        m.onHealPickUp += v.GetHealth;
        m.onLifePickUp += v.GetLife;
        m.onShowRender += v.EnableRenderer;
        m.onRenderColor += v.SetRenderColor;
        m.normalRenderColor += v.NormalRender;
        m.onFlipRender += v.FlipRender;
        m.onXMovement += v.XAxi;
        m.onYVelocity += v.YVelocity;
        m.onYMovement += v.YAxi; // Para el view en el Ladder
        m.onGroundedJump += v.Jump;
        m.onAirJump += v.AirJump;
        m.onLandGround += v.Land;
        m.onLadderGrab += v.LadderGrabbed;
        m.onDetectGround += v.UpdateGroundDetection;
        m.onAttack += v.Shoot;
        m.onGetHit += v.TakeDamage;
        m.onDeath += v.Die;
        m.onGrabChain += v.GrabChain;
        m.addAttackToAnimation += v.SetAttackToAnimator;
        
        //SetController(startController);
    }

    private void ControllerUpdate() {

        _actualController.ListenKeys();
        

        _skinKeys(_model);



        if (Input.GetKeyDown(KeyCode.P)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ControllerFixedUpdate()
    {
        _actualController.ListenFixedKeys();
    }

    void SkinKeys(Action<PlayerModel> keys)
    {
        _skinKeys = keys;
    }

    public void SetController(IController newController)
    {
        _actualController = newController;
    }

    public void DisableControls()
    {
        _model.onUpdate -= ControllerUpdate;
    }

    public void EnableControls()
    {
        _model.onUpdate += ControllerUpdate;
    }
    
}
