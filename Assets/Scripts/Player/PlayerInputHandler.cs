using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _input;

    private Vector2 _moveInput;
    private Vector2 _mousePos;

    public UnityAction OnFreezeTarget = null;

    public UnityAction<bool> OnBlock = null;
    public UnityAction<bool> OnFineControl = null;

    public UnityAction OnAttack;

    public float timeSincePressedJump
    {
        get;
        private set;
    }

    private void OnEnable()
    {
        _input.actions["Move"].performed += OnMoveInput;
        _input.actions["Move"].canceled += OnMoveInput;
        
        _input.actions["Mouse Pos"].performed += OnMousePosInput;

        _input.actions["Freeze Target"].performed += OnFreezeTargetInput;

        _input.actions["Block"].performed += OnBlockInput;
        _input.actions["Block"].canceled += OnBlockInput;
        
        _input.actions["Fine Control"].performed += OnFineControlInput;
        _input.actions["Fine Control"].canceled += OnFineControlInput;
        
        _input.actions["Jump"].performed += OnJumpInput;
        
        _input.actions["Attack"].performed += OnAttackInput;

        LockMouse(true);
    }

    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMoveInput;
        _input.actions["Move"].canceled -= OnMoveInput;
        _input.actions["Mouse Pos"].performed -= OnMousePosInput;

        _input.actions["Freeze Target"].performed -= OnFreezeTargetInput;

        _input.actions["Block"].performed -= OnBlockInput;
        _input.actions["Block"].canceled -= OnBlockInput;
        
        _input.actions["Fine Control"].performed -= OnFineControlInput;
        _input.actions["Fine Control"].canceled -= OnFineControlInput;
        
        _input.actions["Jump"].performed -= OnJumpInput;
        
        _input.actions["Attack"].performed -= OnAttackInput;
    }

    private void Update()
    {
        if (timeSincePressedJump < 5)
            timeSincePressedJump += Time.deltaTime;
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _moveInput = Vector2.zero;
            return;
        }

        _moveInput = context.ReadValue<Vector2>();
    }

    public Vector2 MoveInput()
    {
        return _moveInput;
    }

    private void OnMousePosInput(InputAction.CallbackContext context)
    {
        _mousePos = context.ReadValue<Vector2>();
    }

    public Vector2 GetMousePosition()
    {
        return _mousePos;
    }

    
    private void OnFreezeTargetInput(InputAction.CallbackContext context)
    {
        OnFreezeTarget?.Invoke();
    }


    private void OnBlockInput(InputAction.CallbackContext context)
    {
        OnBlock?.Invoke(context.performed);
    }

    private void OnFineControlInput(InputAction.CallbackContext context)
    {
        if (context.performed)
            LockMouse(false);
        else 
            LockMouse(true);


        OnFineControl?.Invoke(context.performed);
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        timeSincePressedJump = 0;
    }

    private void OnAttackInput(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }
    
    public void LockMouse(bool value)
    {
        if(value)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
}
