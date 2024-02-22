using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
//GameEvent: A Scriptable Object which notifies listeners when the event is raised
public class GameEvent : ScriptableObject
{
    List<GameEventListener> listeners;

    public void RegisterListener(GameEventListener listener) => listeners.Add(listener);
    public void UnregisterListener(GameEventListener listener) => listeners.Remove(listener);
    public void RaiseEvent()
    {
        foreach(GameEventListener listener in listeners)
        {
            listener.EventRaised();
        }
    }
}
