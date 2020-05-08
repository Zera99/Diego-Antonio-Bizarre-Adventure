using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerView : MonoBehaviour {

    Animator _anim;
    SpriteRenderer _renderer;
    Color _oldColor;

    public AudioSource audioSource;
    public AudioClip jumpingSound;
    public AudioClip hurtSound;
    public AudioClip fartSound;
    public AudioClip lifeCollected;
    public AudioClip healthCollected;

    Action _enableControls = delegate { };
    Action _disaleControls = delegate { };
    

    private void Awake() {
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _oldColor = _renderer.color;
        //_anim.runtimeAnimatorController = stats.currentAnimator;
        
    }
    
    /* FRANCO */

    public void SetDisableControlBehaviour(Action disableC)
    {
        _disaleControls = disableC;
    }

    public void SetEnableControlBehaviour(Action enableC)
    {
        _enableControls = enableC;
    }

    public void SetAttackToAnimator(Action atck)
    {
        var atkAnimBehav = _anim.GetBehaviour<AttackAnimExecution>();
        if (atkAnimBehav)
            atkAnimBehav.SetAttackAction(atck);
    }

    public void SetSkin(RuntimeAnimatorController newAnimator)
    {
        _anim.runtimeAnimatorController = newAnimator;

        foreach (var item in _anim.GetBehaviours<DisableControlAnimation>())
        {
            item.SetDisableAction(_disaleControls);
        }
        
        foreach (var item in _anim.GetBehaviours<EnableControlAnimation>())
        {
            item.SetEnableAction(_enableControls);
        }
    }

    public void GetHealth()
    {
        audioSource.PlayOneShot(healthCollected);
    }

    public void GetLife()
    {
        audioSource.PlayOneShot(lifeCollected);
    }

    public void NormalRender()
    {
        _renderer.color = _oldColor;
    }

    public void FlipRender(bool value)
    {
        _renderer.flipX = value;
    }

    public void XAxi(float value)
    {
        _anim.SetFloat("xAxi", value);
    }

    public void YVelocity(float value)
    {
        _anim.SetFloat("airYspeed", value);
    }

    public void YAxi(float value) {
        _anim.SetFloat("yAxi", value);
    }

    public void UpdateGroundDetection(bool value)
    {
        _anim.SetBool("isInAir", !value);
    }

    public void Jump()
    {
        _anim.SetTrigger("Jump");
        audioSource.PlayOneShot(jumpingSound);
    }

    public void AirJump()
    {
        Jump();
    }

    public void Land()
    {
        _anim.SetTrigger("Land");
    }

    public void LadderGrabbed(bool value)
    {
        _anim.SetBool("usingLadder", value);
    }

    public void EnableRenderer(bool value)
    {
        _renderer.enabled = value;
    }

    public void SetRenderColor(Color newCol)
    {
        _renderer.color = newCol;
    }

    public void TakeDamage()
    {
        _anim.SetTrigger("GetHit");
        PlayHurtSound();
    }

    public void Die()
    {
        _anim.SetTrigger("Death");
    }

    public void GrabChain(bool value)
    {
        _anim.SetBool("GrabChain", value);
    }

    public void Shoot() {
        _anim.SetTrigger("Attack");
    }

    /* END FRANCO */
    


    public void PlayJumpSound() {
        audioSource.PlayOneShot(jumpingSound);
    }

    public void PlayHurtSound() {
        audioSource.PlayOneShot(hurtSound);
    }

    public void PlayFartSound() {
        audioSource.PlayOneShot(fartSound);
    }

    public void PlayHpCollected() {
        audioSource.PlayOneShot(healthCollected);
    }

    public void PlayLifeCollected() {
        audioSource.PlayOneShot(lifeCollected);
    }
    
    



    
    
}