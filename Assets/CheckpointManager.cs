using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    private Vector3 _currentCheckpoint = new Vector3(0, 1, 0);

    private Transform _player;

    private void OnEnable()
    {
        instance = this;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SetCheckpoint(Vector3 checkpoint)
    {
        _currentCheckpoint = checkpoint;
    }

    public void TeleportPlayerToCheckpoint()
    {
        DeathResetScreen.instance.TriggerScreen();
        _player.GetComponent<PlayerMovement>().ResetVelocity();
        _player.GetComponent<CharacterController>().enabled = false;
        _player.position = _currentCheckpoint;
        _player.GetComponent<CharacterController>().enabled = true;
    }
}
