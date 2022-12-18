using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    /// <summary>
    /// This is state controller of state machine system.
    /// </summary>
    public class StateController<T> : MonoBehaviour
    {
        protected State<T> curretState { get; private set; }

        private Dictionary<T, Func<bool>> stateFillter = new Dictionary<T, Func<bool>>();

        /// <summary>
        /// Add State condition fillter check every time on change to this type of state.
        /// </summary>
        /// <param name="fillterType"></param>
        /// <param name="conditionFillter"></param>
        public void SetStateFillter(T fillterType, Func<bool> conditionFillter)
        {
            if (stateFillter.ContainsKey(fillterType))
                stateFillter[fillterType] = conditionFillter;
            else
                stateFillter.Add(fillterType, conditionFillter);
        }

        /// <summary>
        /// Call for change state to target state.
        /// </summary>
        /// <param name="targetState"></param>
        public virtual void Change(State<T> targetState)
        {
            if(stateFillter.ContainsKey(targetState.type))
            {
                var fillterValidable = stateFillter[targetState.type];
                if (!fillterValidable.Invoke())
                    return;
            }

            if (curretState != null)
                curretState.Exit();

            curretState = targetState;
            curretState.Init(this);
            curretState.Enter();
        }

        private void Update()
        {
            curretState?.Update();

            OnUpdate();
        }

        /// <summary>
        /// Invoke every frame.
        /// </summary>
        protected virtual void OnUpdate() { }
    }
}
