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
        _anim = this.transform.Find("Art").Find("character1").GetComponent<Animator>();
    }

    private void Update()
    {
        if (_anim != null && _anim.isActiveAndEnabled)
        {
            _crouch = _anim.GetBool("Crouching");
            _spinDash = _anim.GetFloat("Speed") > 1;
            if (_anim.GetFloat("Speed") <= 1 || _crouch == true)
            {
                _activateSpinDash = false;
            }
        }
        if (_anim.GetFloat("Speed") == 0)
            _rb.velocity = new Vector3(0, 0, 0);
        //Debug.Log(_rb.velocity);
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
            //Debug.Log("Applied force");
            if (_anim.isActiveAndEnabled)
            {
                _rb.velocity = (5f * this.transform.right * _anim.GetFloat("Speed"));
            }
            _activateSpinDash = true;
        }
        if (spinning == true && _anim.isActiveAndEnabled)
        {
            _anim.transform.Rotate(new Vector3(_anim.GetFloat("Speed") * 5f, 0, 0), Space.Self);
            //_anim.transform.localPosition.y = Math.Sin(_anim.GetFloat("Speed"));
        }
    }
}
