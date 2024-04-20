using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker instance;

    private float _currentMagnitude = 0;
    float _timeToShake = 0;
    private float _currentDamping = 0;
    private bool _isShaking = false;

    [SerializeField]
    private CinemachineCameraOffset _offset;

    private Vector3 _originalOffset;


    private void Awake()
    {
        instance = this;
        _originalOffset = _offset.m_Offset;
    }


    public void TriggerShake(float duration, float magnitude, float damping)
    {
        if(magnitude > _currentMagnitude)
        {
            _currentMagnitude = magnitude;
            _currentDamping = damping;
            _timeToShake = duration;
            _isShaking = true;
        }
    }

    public void CancleShake(float magnitudeThreshold)
    {
        if (_currentMagnitude <= magnitudeThreshold)
        {
            _isShaking = false;
            _currentMagnitude = 0;
            _offset.m_Offset = _originalOffset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( _isShaking )
        {
            _timeToShake -= Time.deltaTime;
            Vector3 randomOffset = Random.insideUnitCircle * _currentMagnitude;
            _offset.m_Offset = Vector3.Lerp(_offset.m_Offset, _originalOffset + randomOffset, 1 - _currentDamping);

            if(_timeToShake <= 0)
            {
                _isShaking = false;
                _currentMagnitude = 0;
                _offset.m_Offset = _originalOffset;
            }
        }
    }
}
