using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    PlayerInput _input = null;
    PlayerMotor _motor = null;

    [SerializeField] float _moveSpeed = 0.1f;
    float _spinSpeed = 0.1f;

    void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _motor = GetComponent<PlayerMotor>();
    }

    private void OnEnable()
    {
        _input.MoveInput += OnMove;
        _input.CrouchInput += OnCrouch;
        _input.SpinDashInput += OnSpinDash;
    }

    private void OnDisable()
    {
        _input.MoveInput -= OnMove;
        _input.CrouchInput -= OnCrouch;
        _input.SpinDashInput -= OnSpinDash;
    }

    void OnMove(Vector3 movement)
    {
        _motor.Move(movement * _moveSpeed);
    }

    void OnCrouch()
    {
        _motor.Crouch();
    }

    void OnSpinDash()
    {
        _spinSpeed = _input._chargedSpeed;
        //Debug.Log(_spinSpeed);
        _motor.SpinDash(_spinSpeed);
    }
}
