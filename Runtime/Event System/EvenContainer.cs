using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace Services.EventsSystem
{
    [AddComponentMenu("Service/Utilities/EvenContainer")]
    public class EvenContainer : MonoBehaviour
    {
        [Serializable]
        class EventSlot
        {
            public string eventName;
            [SerializeField] UnityEvent eventAction;

            public void Invoke() => eventAction?.Invoke();
        }

        [SerializeField] List<EventSlot> eventSlots = new List<EventSlot>();

        public void InvokeEvent(string name)
        {
            foreach (EventSlot slot in eventSlots) 
                if (slot.eventName == name) 
                    slot.Invoke();
        }

        public void InvokeEventIndex(int index) => eventSlots[index].Invoke();
    }
}