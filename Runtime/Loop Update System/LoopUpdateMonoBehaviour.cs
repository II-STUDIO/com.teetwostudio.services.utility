using UnityEngine;

namespace Services
{
    public abstract class LoopUpdateMonoBehaviour : MonoBehaviour, ILoopUpdateEntity
    {
        public bool IsUpdatable
        {
            get => m_isFirstFrameEntry && isActiveAndEnabled;
        }

        private bool m_isFirstFrameEntry = false;

        protected virtual void Start()
        {
            m_isFirstFrameEntry = true;
        }

        protected virtual void OnEnable()
        {
            LoopUpdater.EnableEntity(this);
        }

        protected virtual void OnDisable()
        {
            LoopUpdater.DisableEntity(this);
        }

        public abstract void LoopUpdateEvent(float deltaTime);
    }
}