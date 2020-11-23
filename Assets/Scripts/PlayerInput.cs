using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public float _chargedSpeed = 0;
    public bool _crouching = false;
    bool _isRolling = false;
    int _facingSide = 0;
    Animator _anim = null;
    Quaternion _facing = new Quaternion();

    public event Action<Vector3> MoveInput = delegate { };
    public event Action CrouchInput = delegate { };
    public event Action SpinDashInput = delegate { };

    private void Awake()
    {
        _anim = GameObject.Find("Art").GetComponent<Animator>();
    }

    void Update()
    {
        if (_chargedSpeed > 0)
        {
            _chargedSpeed -= 0.02f;
        }
        if (_chargedSpeed < 0)
        {
            _chargedSpeed = 0;
        }
        //Debug.Log(_chargedSpeed);
        DetectMoveInput();
        _anim.SetFloat("Speed", _chargedSpeed + 1);
        _anim.SetBool("Crouching", _crouching);
        _crouching = false;

    }

    private void DetectMoveInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        if (xInput != 0)
        {
            if (xInput > 0)
            {
                _facing.Set(0f, 0f, 0f, 1);
                _facingSide = 1;
            }
            else
            {
                _facing.Set(0f, 180f, 0f, 1);
                _facingSide = -1;
            }
            this.transform.rotation = _facing;
            if (yInput != -1)
            {
                Vector3 _horizontalMovement = transform.right * xInput * _facingSide;
                MoveInput?.Invoke(_horizontalMovement);
            }
        }
        if (yInput != 0)
        {
            if (yInput > 0)
            {

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    if (_chargedSpeed < 10)
                        _chargedSpeed += 1.5f;
                    SpinDashInput?.Invoke();
                }
                _crouching = true;
                CrouchInput?.Invoke();
            }
        }
    }
}
