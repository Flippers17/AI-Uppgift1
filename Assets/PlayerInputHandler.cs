using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _input;

    private Vector2 _moveInput;


    private void OnEnable()
    {
        _input.actions["Move"].performed += OnMoveInput;
        _input.actions["Move"].canceled += OnMoveInput;
        LockMouse(true);
    }

    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMoveInput;
        _input.actions["Move"].canceled -= OnMoveInput;
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



    public void LockMouse(bool value)
    {
        if(value)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
}
