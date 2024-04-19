using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdsObstacleCourse : MonoBehaviour
{
    
    private int _checkpointsLeft = 0;

    public UnityEvent OnCourseComplete;
    [HideInInspector]
    public bool courseCompleted = false;

    public void AddCheckpoint()
    {
        if(courseCompleted)
            return;

        _checkpointsLeft++;
    }

    public void RemoveCheckpoint()
    {
        if (courseCompleted)
            return;


        _checkpointsLeft--;

        if( _checkpointsLeft <= 0)
        {
            OnCourseComplete?.Invoke();
            courseCompleted = true;
        }
    }

}

