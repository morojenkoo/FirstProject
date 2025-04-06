using System;
using System.Collections.Generic;
using FirstProject.Exceptions;
using UnityEngine;

namespace FirstProject.FSM
{
    public class BaseStateMachine
    {
        private BaseState _currentState;
        private List<BaseState> _states;
        private Dictionary<BaseState, List<Transition>> _transitions;

        public BaseStateMachine()
        {
            _states = new List<BaseState>();
            _transitions = new Dictionary<BaseState, List<Transition>>();
        }

        public void SetInitialState(BaseState State)
        {
            _currentState = State;
        }

        public void AddState(BaseState state, List<Transition> transitions)
        {
            if (!_states.Contains(state))
            {
                _states.Add(state);
                _transitions.Add(state, transitions);
            }
            else
            {   
                throw new AlreadyExistsException($"State {state.GetType()} already exists in state machine!");
            }
        }

        public void Update()
        {
            foreach (var transition in _transitions[_currentState])
            {
                if (transition.Condition())
                {
                    Debug.Log($"State {_currentState.GetType()} is transitioning to {transition.ToState.GetType()}");
                    _currentState = transition.ToState;
                    break;
                }
            }
            _currentState.Execute();
        }
    }
}