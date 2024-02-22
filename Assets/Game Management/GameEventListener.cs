using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//GameEventListener: a mono behavior component. Registers as a listener to a gameevent. 
//invokes a unity event when game event is raised
public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    public UnityEvent eventRaised = new UnityEvent();

    private void Awake()
    {
        if (gameEvent) gameEvent.RegisterListener(this);
    }

    void OnDestroy()
    {
        if (gameEvent) gameEvent.UnregisterListener(this);
    }

    public void EventRaised()
    {
        eventRaised.Invoke(); 
    }
}
