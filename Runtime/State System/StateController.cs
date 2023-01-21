using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services.StateMachine
{
    /// <summary>
    /// This is state controller of state machine system.
    /// </summary>
    public class StateController<StateType, Property>
    {
        /// <summary>
        /// The property that your can custom to very state you create that accessable for the property.
        /// </summary>
        public Property property { get; private set; }

        public StateType priviousStateType { get; private set; }

        protected State<StateType, Property> curretState { get; private set; }

        private Dictionary<StateType, State<StateType, Property>> StateConten = new Dictionary<StateType, State<StateType, Property>>();

        public void SetConten(State<StateType, Property> state)
        {
            if(StateConten.ContainsKey(state.type))
            {
                Debug.LogWarningFormat($"State type <{state.type}> aready set in the state conten of the controller");
                return;
            }

            StateConten.Add(state.type, state);
        }

        public void SetProperty(Property property)
        {
            this.property = property;
        }

        /// <summary>
        /// Call for change state to target state.
        /// </summary>
        /// <param name="stateType"></param>
        public virtual void Change(StateType stateType)
        {
            if (curretState != null)
            {
                priviousStateType = curretState.type;
                curretState.Exit();
            }

            curretState = StateConten[stateType];

            if(curretState == null)
            {
                Debug.LogErrorFormat($"State type <{stateType}> not extis or set conten for the controller");
                return;
            }

            curretState.Init(this);
            curretState.Enter();
        }

        public virtual void Update(float deltaTime)
        {
            if(curretState == null)
                return;

            curretState.Update(deltaTime);
        }
    }
}
