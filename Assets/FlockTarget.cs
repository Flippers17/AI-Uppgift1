using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlockTarget : MonoBehaviour
{
    [SerializeField]
    private InputAction mousePos;
    [SerializeField]
    private InputAction freezeButton;

    [SerializeField]
    private TargetSteeringBehaviour _targetSteeringBehaviour;

    [SerializeField]
    private LayerMask mask;

    private Camera _cam;

    private bool _updatePosition = true;

    private void OnEnable()
    {
        if (_targetSteeringBehaviour)
            _targetSteeringBehaviour.Target = transform;

        _cam = Camera.main;

        mousePos.Enable();
        freezeButton.Enable();

        mousePos.performed += UpdatePosition;
        freezeButton.performed += ToggleFreeze;
    }

    private void OnDisable()
    {
        mousePos.performed -= UpdatePosition;
        freezeButton.performed -= ToggleFreeze;
        mousePos.Disable();
        freezeButton.Disable();
    }

    private void UpdatePosition(InputAction.CallbackContext ctx)
    {
        if (!_updatePosition)
            return;

        Ray ray = _cam.ScreenPointToRay(mousePos.ReadValue<Vector2>());

        if(Physics.Raycast(ray,out RaycastHit hit, 100, mask))
        {
            transform.position = hit.point + 3 * Vector3.up;
        }
    }

    private void ToggleFreeze(InputAction.CallbackContext ctx)
    {
        _updatePosition = !_updatePosition;
    }
}
