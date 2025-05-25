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

        public bool IsProcessing { get; private set; }

        public void SetListener(EventAction onBegin, EventAction onComplete)
        {
            _onBegin = onBegin;
            _onComplete = onComplete;
        }

        public virtual void Begin()
        {
            if (IsProcessing) return;

            _onBegin?.Invoke();
            IsProcessing = true;
        }

        public virtual void Completed()
        {
            if (!IsProcessing) return;

            _onComplete?.Invoke();
            IsProcessing = false;
        }

        public virtual void ForceCompleted()
        {
            Completed();
        }
    }
}
