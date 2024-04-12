using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    [SerializeField] private bool _limitFPS = true;
    [SerializeField]
    private int _fps = 60;


    private void OnEnable()
    {
        if (!_limitFPS)
        {
            Application.targetFrameRate = -1;
            return;
        }
        
        Application.targetFrameRate = _fps;
    }
}
