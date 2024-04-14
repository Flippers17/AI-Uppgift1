using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    private Vector3 _currentCheckpoint = Vector3.zero;

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
        _player.position = _currentCheckpoint;
    }
}
