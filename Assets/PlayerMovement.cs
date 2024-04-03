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

    private Vector3 _velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        MovePlayer();
    }

    private void HandleMovement()
    {
        _velocity.y = -1;
        Vector2 moveInput = _input.MoveInput();

        _velocity.x = moveInput.x * _moveSpeed;
        _velocity.z = moveInput.y * _moveSpeed;
    }


    private void MovePlayer()
    {
        _controller.Move(_velocity * Time.deltaTime);
    }
}
