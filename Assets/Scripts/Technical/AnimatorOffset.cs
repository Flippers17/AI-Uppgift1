using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimatorOffset : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        _animator.SetFloat("Cycle Offset", Random.Range(0f, 1f));
    }
}
