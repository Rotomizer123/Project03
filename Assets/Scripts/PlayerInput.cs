using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] ParticleSystem _speedUpParticles = null;
    [SerializeField] AudioClip _speedUp1 = null;
    [SerializeField] AudioClip _speedUp2 = null;
    [SerializeField] AudioClip _speedUp3 = null;
    [SerializeField] AudioClip _speedUp4 = null;
    [SerializeField] AudioClip _spinDashSound = null;
    [SerializeField] AudioClip _footSteps = null;

    public float _maxSpeed = 12;

    public float _chargedSpeed = 0;
    public bool _crouching = false;
    bool _isRolling = false;
    bool _isRunning = false;
    int _facingSide = 0;
    Animator _anim = null;
    Animator _anim1 = null;
    ParticleSystem _charging = null;
    Quaternion _facing = new Quaternion();

    public event Action<Vector3> MoveInput = delegate { };
    public event Action CrouchInput = delegate { };
    public event Action SpinDashInput = delegate { };

    private void Awake()
    {
        _anim = this.transform.Find("Art").Find("character").GetComponent<Animator>();
        _anim1 = this.transform.Find("Art").Find("character1").GetComponent<Animator>();
        _charging = this.transform.Find("SpeedingUp").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (_chargedSpeed > 0)
        {
            _chargedSpeed -= 0.04f;
        }
        if (_chargedSpeed < 2)
        {
            this.transform.Find("Art").Find("character").gameObject.SetActive(true);
            this.transform.Find("Art").Find("character1").gameObject.SetActive(false);
            _anim1.gameObject.SetActive(false);
            _isRolling = false;
            _chargedSpeed = 0;
        }
        //Debug.Log(_chargedSpeed);
        DetectMoveInput();
        if (_anim.isActiveAndEnabled)
        {
            _anim.SetFloat("Speed", _chargedSpeed);
            _anim.SetBool("Crouching", _crouching);
            _anim.SetBool("Running", _isRunning);
        }
        if (_anim1.isActiveAndEnabled)
        {
            if (_chargedSpeed == 0)
                _anim1.SetFloat("Speed", _chargedSpeed);
            else
                _anim1.SetFloat("Speed", _chargedSpeed + 1);
            _anim1.SetBool("Crouching", _crouching);
        }
        if (_isRunning && GameObject.Find("Footsteps") == null)
        {
            AudioHelper.PlayClip2D(_footSteps, 0.5f, 1);
        }
        _crouching = false;
        _isRunning = false;
    }

    private void DetectMoveInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        if (xInput != 0 && !_crouching)
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
            if (yInput != -1 && !_isRolling)
            {
                this.transform.rotation = _facing;
                Vector3 _horizontalMovement = transform.right * xInput * _facingSide;
                MoveInput?.Invoke(_horizontalMovement);
                _isRunning = true;
            }
        }
        if (yInput != 0 && !_isRolling)
        {
            if (yInput < 0)
            {
                _crouching = true;
                CrouchInput?.Invoke();
                if (Input.GetKeyDown(KeyCode.M))
                {
                    if (_chargedSpeed < _maxSpeed)
                    {
                        _chargedSpeed += 2.5f;
                        if (_chargedSpeed > 2 && _chargedSpeed < 4 && _speedUp1 != null)
                        {
                            AudioHelper.PlayClip2D(_speedUp1, 1, 1);
                        }
                        else if (_chargedSpeed > 4 && _chargedSpeed < 6 && _speedUp2 != null)
                        {
                            AudioHelper.PlayClip2D(_speedUp2, 1, 1);
                        }
                        else if (_chargedSpeed > 6 && _chargedSpeed < 8 && _speedUp3 != null)
                        {
                            AudioHelper.PlayClip2D(_speedUp3, 1, 1);
                        }
                        else if (_chargedSpeed > 8 && _chargedSpeed < 11 && _speedUp4 != null)
                        {
                            AudioHelper.PlayClip2D(_speedUp4, 1, 1);
                        }
                    }
                    SpinDashInput?.Invoke();
                    _charging.Emit(1);
                    this.transform.Find("Art").Find("character").gameObject.SetActive(false);
                    this.transform.Find("Art").Find("character1").gameObject.SetActive(true);
                    _anim1.gameObject.SetActive(true);

                    _speedUpParticles.Emit(15);
                }
            }
        }
        if (!_crouching && _chargedSpeed > 2)
        {
            _isRolling = true;
        }
        if (GameObject.Find("SpinDash") == null && _isRolling && !_crouching && _chargedSpeed > 4)
        {
            AudioHelper.PlayClip2D(_spinDashSound, 1, _chargedSpeed);
        }
    }
}
