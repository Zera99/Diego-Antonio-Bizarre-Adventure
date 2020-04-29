using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Generic Event")]
public class EventTrigger : ScriptableObject {

    public List<EventObject> listeners = new List<EventObject>();

    private void OnEnable() {
        listeners.Clear();
    }


    public void TriggerEvents() {
        if (listeners.Count == 0)
            return;

        for(int i = listeners.Count-1; i >= 0; i--) {
            listeners[i].TriggerEvent();
        }
    }

    public void AddObject(EventObject o) {
        listeners.Add(o);
    }

    public void RemoveObject(EventObject o) {
        listeners.Remove(o);
    }

}
