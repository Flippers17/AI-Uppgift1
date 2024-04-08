using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerInputHandler _input;

    [SerializeField]
    private CharacterController _controller;

    [SerializeField]
    private float _moveSpeed = 6f;
    
    [SerializeField]
    private float _gravity = 40f;

    [SerializeField] private Transform _groundCheck;

    [SerializeField] private LayerMask _groundLayers;
    [SerializeField] private float _groundCheckRadius;

    private bool _isGrounded = false;
    private float timeSinceJumped = 1f;

    [SerializeField] private float _jumpInputRememberTime = .2f;
    [SerializeField] private float _jumpHeight = 3f;
    private float _jumpVelocity;
    
    
    private Vector3 _velocity = Vector3.zero;

    private Camera _cam;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _jumpVelocity = Mathf.Sqrt(2 * _gravity * _jumpHeight);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleGravity();
        MovePlayer();
    }

    private void FixedUpdate()
    {
        _isGrounded = IsGrounded();
    }

    private void HandleMovement()
    {
        //_velocity.y = -1;
        Vector2 moveInput = _input.MoveInput();
        Vector3 forward = _cam.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = _cam.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 movementVector = forward * (moveInput.y * _moveSpeed) + right * (moveInput.x * _moveSpeed);
        _velocity.x = movementVector.x;
        _velocity.z = movementVector.z;
        
        if (timeSinceJumped < 1)
            timeSinceJumped += Time.deltaTime;
        
        if(_input.timeSincePressedJump < _jumpInputRememberTime)
            HandleJump();
    }

    private void HandleJump()
    {
        if(!_isGrounded || timeSinceJumped < .2f)
            return;

        _velocity.y = _jumpVelocity;
        timeSinceJumped = 0;
    }
    
    private void HandleGravity()
    {
        if (!_isGrounded)
            _velocity.y -= Time.deltaTime * _gravity;
        else if(timeSinceJumped > .2f)
            _velocity.y = -1;
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundLayers);
    }

    private void MovePlayer()
    {
        _controller.Move(_velocity * Time.deltaTime);
    }
}
