using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    Vector3 _movementThisFrame = Vector3.zero;
    bool _crouch = false;
    bool _spinDash = false;
    bool _activateSpinDash = false;
    Animator _anim = null;

    Rigidbody _rb = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GameObject.Find("Art").GetComponent<Animator>();
    }

    private void Update()
    {
        if (_anim != null)
        {
            _crouch = _anim.GetBool("Crouching");
            _spinDash = _anim.GetFloat("Speed") > 1;
            if (_anim.GetFloat("Speed") <= 1 || _crouch == true)
            {
                _activateSpinDash = false;
            }
        }
            
    }

    void FixedUpdate()
    {
        ApplyMovement(_movementThisFrame);
        ApplySpinDash(_spinDash);
    }

    public void Move(Vector3 requestedMovement)
    {
        _movementThisFrame = requestedMovement;
    }

    void ApplyMovement(Vector3 moveVector)
    {
        if (moveVector == Vector3.zero)
            return;
        _rb.MovePosition(_rb.position + moveVector);
        _movementThisFrame = Vector3.zero;
    }

    void ApplySpinDash(bool spinning)
    {
        if (spinning == true && _crouch == false && !_activateSpinDash) 
        {
            Debug.Log("Applied force");
            _rb.velocity = (5f * this.transform.right * _anim.GetFloat("Speed"));
            _activateSpinDash = true;
        }
    }
}
