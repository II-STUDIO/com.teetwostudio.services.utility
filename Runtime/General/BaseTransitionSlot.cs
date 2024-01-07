using Services.EventsSystem;
using System;
using UnityEngine;

namespace Services
{
    [Serializable]
    public abstract class BaseTransitionSlot : MonoBehaviour
    {
        private EventAction _onBegin;
        private EventAction _onComplete;

        public abstract float Time { get; }

        public void SetListener(EventAction onBegin, EventAction onComplete)
        {
            _onBegin = onBegin;
            _onComplete = onComplete;
        }

        public virtual void Begin()
        {
            _onBegin?.Invoke();
        }

        public virtual void Completed()
        {
            _onComplete?.Invoke();
        }
    }
}