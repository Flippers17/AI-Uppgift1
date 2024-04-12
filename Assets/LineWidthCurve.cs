using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineWidthCurve : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private float _widthMultiplier = 1f;

    [SerializeField]
    private float _duration = 1f;
    private float _timeRatio = 1f;
    private float _timer = 0f;

    public UnityEvent OnCurveEnded;


    private void OnEnable()
    {
        _timeRatio = 1/ _duration;
        _timer = _duration;
    }


    // Update is called once per frame
    void Update()
    {
        if(_timer > 0f)
        {
            float width = curve.Evaluate((_duration - _timer) *_timeRatio) * _widthMultiplier;
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
        }
        else
            OnCurveEnded?.Invoke();

        _timer -= Time.deltaTime;
    }
}
