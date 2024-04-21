using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathResetScreen : MonoBehaviour
{
    public static DeathResetScreen instance;

    [SerializeField]
    private Animator _animator;

    private void Awake()
    {
        instance = this;
    }

    

    public void TriggerScreen()
    {
        _animator.Play(0);
    }
}
