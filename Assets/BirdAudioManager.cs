using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _AudioSource;

    [SerializeField, Range(-3, 3)]
    private float _maxPitch = 1f;
    [SerializeField, Range(-3, 3)]
    private float _minPitch = 0f;

    [SerializeField, Range(0, 1)]
    private float _cycleOffset = 0f;
    [SerializeField]
    private bool _randomizeOffset = true;

    [SerializeField, Range(0, 10)]
    private float _maxCycleLength = 1f;
    [SerializeField, Range(0, 10)]
    private float _minCycleLenght = 1f;
    private float _cycleLength = 0f;

    private float _timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        _cycleLength = Random.Range(_minCycleLenght, _maxCycleLength);
        _cycleOffset = Random.Range(0f, 1f);
        _timer = _cycleLength * _cycleOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if( _timer > 0f)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            PlaySound();
            _timer = _cycleLength;
        }
    }


    private void PlaySound()
    {
        _AudioSource.pitch = Random.Range(_minPitch, _maxPitch);
        _AudioSource.Play();
    }
}
