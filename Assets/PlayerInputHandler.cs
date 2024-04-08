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


    private void OnEnable()
    {
        _input.actions["Move"].performed += OnMoveInput;
        _input.actions["Move"].canceled += OnMoveInput;
        
        _input.actions["Mouse Pos"].performed += OnMousePosInput;

        _input.actions["Freeze Target"].performed += OnFreezeTargetInput;

        _input.actions["Block"].performed += OnBlockInput;
        _input.actions["Block"].canceled += OnBlockInput;

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
        Debug.Log("Doing input");
    }


    private void OnBlockInput(InputAction.CallbackContext context)
    {
        OnBlock?.Invoke(context.performed);
    }


    public void LockMouse(bool value)
    {
        if(value)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
}
