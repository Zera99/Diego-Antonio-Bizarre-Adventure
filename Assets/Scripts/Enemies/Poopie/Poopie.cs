﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poopie : MonoBehaviour {

    public float TimeToJump;
    public float JumpForce;
    public int Damage;
    public Transform WPLeft;
    public Transform WPRight;
    Animator _anim;
    SpriteRenderer _renderer;
    float _internalTime;
    Rigidbody2D _rb;
    bool _jumping;
    bool _isFlipped;
    bool _hasJumped;

    private void Awake() {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start() {
        _internalTime = 0.0f;
        _jumping = false;
        _hasJumped = false;
    }

    // Update is called once per frame
    void Update() {
        _internalTime += Time.deltaTime;
        if (_internalTime >= TimeToJump && !_jumping) {
            _jumping = true;
            _hasJumped = true;
            _anim.SetBool("Jumping", true);
            if (_isFlipped) {
                _rb.AddForce(new Vector2(1.0f, 1.0f).normalized * JumpForce, ForceMode2D.Impulse);
            } else {
                _rb.AddForce(new Vector2(-1.0f, 1.0f).normalized * JumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        PlayerModel p = collision.gameObject.GetComponent<PlayerModel>();
        if (p != null) {
            p.TakeDamage(Damage);
            PoopieHit();
        } else if (transform.position.x > WPRight.transform.position.x) {
            FlipPoopie(false);
        } else if (transform.position.x < WPLeft.transform.position.x) {
            FlipPoopie(true);
        }
        _jumping = false;
        _anim.SetBool("Jumping", false);
        _internalTime = 0.0f;
    }

    void PoopieHit() {
        if (_isFlipped) {
            _rb.AddForce(new Vector2(1.0f, 1.0f).normalized * JumpForce / 2, ForceMode2D.Impulse);
        } else {
            _rb.AddForce(new Vector2(-1.0f, 1.0f).normalized * JumpForce / 2, ForceMode2D.Impulse);
        }
        FlipPoopie(!_isFlipped);
    }

    void FlipPoopie(bool val) {
        _isFlipped = val;
        _renderer.flipX = _isFlipped;
    }

}