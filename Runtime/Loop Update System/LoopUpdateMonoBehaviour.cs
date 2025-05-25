using UnityEngine;

namespace Services
{
    public abstract class LoopUpdateMonoBehaviour : MonoBehaviour, ILoopUpdateEntity
    {
        private bool _hasStarted = false;

        /// <summary>
        /// Indicates if this entity is ready for update:
        /// Must have started and be active/enabled.
        /// </summary>
        public bool IsUpdatable => _hasStarted && isActiveAndEnabled;

        protected virtual void Start()
        {
            _hasStarted = true;
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
