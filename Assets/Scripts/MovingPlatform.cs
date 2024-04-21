using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[SelectionBase]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    internal List<Vector3> _positions = new List<Vector3>();
    private int _currentIndex = 0;

    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _waitTime = 5f;

    [SerializeField]
    private bool _triggerFromStart = true;
    [SerializeField]
    private bool _loop = true;

    private bool _moving = false;
    private bool _hasBeenTriggerd = false;

    private CharacterController _playerOnPlatform;


    private void Awake()
    {
        if (_triggerFromStart)
            TriggerMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerOnPlatform = other.GetComponent<CharacterController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerOnPlatform = null;
        }
    }


    public void TriggerMovement()
    {
        if (_hasBeenTriggerd)
            return;

        if(_positions.Count > 1)
        {
            transform.position = _positions[0];
            _currentIndex = 1;
            _moving = true;
            _hasBeenTriggerd = true;
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!_moving)
            return;

        float currentSpeed = _speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, _positions[_currentIndex], currentSpeed);
        Vector3 movingDir = _positions[_currentIndex] - transform.position;

        if (_playerOnPlatform)
            _playerOnPlatform.Move(movingDir.normalized * currentSpeed);

        if(Vector3.SqrMagnitude(movingDir) < 2f)
        {
            _moving = false;

            if (_loop)
            {
                _currentIndex++;
                _currentIndex %= _positions.Count;
                StartCoroutine(WaitBetweenPositions());
            }
        }
    }

    private IEnumerator WaitBetweenPositions()
    {
        yield return new WaitForSeconds(_waitTime);
        _moving = true;
    }
}


[CustomEditor(typeof(MovingPlatform))]
public class MovingPlatformEditor : Editor
{

    private void OnSceneGUI()
    {
        MovingPlatform platform = (MovingPlatform)target;

        
        for(int i = 0; i < platform._positions.Count; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 newTargetPos = Handles.PositionHandle(platform._positions[i], Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(platform, "Moved position");
                platform._positions[i] = newTargetPos;
                EditorUtility.SetDirty(target);
            }
        }
    }
}
