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

    Rigidbody _rb = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ApplyMovement(_movementThisFrame);
        ApplyCrouch(_crouch);
        ApplySpinDash(_spinDash);
    }

    public void Move(Vector3 requestedMovement)
    {
        _movementThisFrame = requestedMovement;
    }

    public void Crouch()
    {
        _crouch = true;
        Debug.Log("Crouch");
    }

    public void SpinDash(float requestedSpeed)
    {
        _spinDash = true;
        Debug.Log("SpinDash");
    }

    void ApplyMovement(Vector3 moveVector)
    {
        if (moveVector == Vector3.zero)
            return;
        _rb.MovePosition(_rb.position + moveVector);
        _movementThisFrame = Vector3.zero;
    }

    void ApplyCrouch(bool crouching)
    {

    }

    void ApplySpinDash(bool spinning)
    {

    }
}
