using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHandler : MonoBehaviour
{
    public List<UnityEvent> events;

    public void TriggerEvent(int index)
    {
        if(events == null || events.Count <= index)
            return;

        events[index]?.Invoke();
    }
}
