using UnityEngine;

namespace StateMachine
{
    /// <summary>
    /// Base state action class.
    /// </summary>
    public abstract class State<T>
    {
        /// <summary>
        /// Type of state action.
        /// </summary>
        public abstract T type { get; }

        protected StateController<T> controller;
        /// <summary>
        /// Invoke when this state was changed with state controller.
        /// </summary>
        /// <param name="controller"></param>
        public virtual void Init(StateController<T> controller) => this.controller = controller;

        /// <summary>
        /// Invoke when state controller changed to this state.
        /// </summary>
        public abstract void Enter();

        /// <summary>
        /// Invoke every frame after enter this state.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Invoke when state controller changed this to other state.
        /// </summary>
        public abstract void Exit();
    }
}
