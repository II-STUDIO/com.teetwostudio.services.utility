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

        public bool isProcessing { get; private set; } = false;

        public void SetListener(EventAction onBegin, EventAction onComplete)
        {
            _onBegin = onBegin;
            _onComplete = onComplete;
        }

        public virtual void Begin()
        {
            _onBegin?.Invoke();

            isProcessing = true;
        }

        public virtual void Completed()
        {
            _onComplete?.Invoke();

            isProcessing = false;
        }

        public virtual void ForceCompleted()
        {
            Completed();
        }
    }
}