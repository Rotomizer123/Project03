using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public float _chargedSpeed = 0;
    public bool _crouching = false;
    Animator _anim = null;

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
            _chargedSpeed -= 0.01f;
        }
        if (_chargedSpeed < 0)
        {
            _chargedSpeed = 0;
        }
        Debug.Log(_chargedSpeed);
        DetectMoveInput();
        _anim.SetFloat("Speed", _chargedSpeed);
        _anim.SetBool("Crouching", _crouching);
        _crouching = false;

    }

    private void DetectMoveInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        if (xInput != 0)
        {
            Vector3 _horizontalMovement = transform.right * xInput;
            MoveInput?.Invoke(_horizontalMovement);
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
                        _chargedSpeed += 1;
                    SpinDashInput?.Invoke();
                }
                _crouching = true;
                CrouchInput?.Invoke();
            }
        }
    }
}
