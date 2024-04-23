using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugBreaker : MonoBehaviour
{
    [SerializeField]
    private InputAction _breakAction;

    private void OnEnable()
    {
        _breakAction.Enable();
        _breakAction.performed += DoBreak;
    }

    private void OnDisable()
    {
        _breakAction.Disable();
        _breakAction.performed -= DoBreak;
    }


    private void DoBreak(InputAction.CallbackContext ctx)
    {
        Debug.Break();
    }
}
