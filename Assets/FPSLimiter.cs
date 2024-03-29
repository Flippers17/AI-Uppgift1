using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    [SerializeField]
    private int _fps = 60;

    private void OnEnable()
    {
        Application.targetFrameRate = _fps;
    }
}
