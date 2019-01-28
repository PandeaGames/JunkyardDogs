using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PandeaGames.ViewModels
{
    public abstract class AbstractStatefulViewModel<T> : AbstractViewModel where T:IConvertible
    {
        public delegate void ViewModelStateDelegate(T state);
        public delegate void ViewModelStateChangeDelegate(T oldState, T newState);

        public event ViewModelStateDelegate OnLeaveState;
        public event ViewModelStateDelegate OnEnterState;
        public event ViewModelStateChangeDelegate OnStateChange;
        
        protected T _currentState;
        protected bool _canChangeState = true;

        public T CurrentState
        {
            get { return _currentState; }
        }

        protected virtual void Start()
        {
            SetState(default(T), true);
        }

        private void SetState(T state, bool isInitialState)
        {
            
            if (_canChangeState && (!state.Equals(_currentState) || isInitialState))
            {
                T oldState = _currentState;
                LeaveState(_currentState);
                _currentState = state;
                StateChange(oldState, _currentState);
                EnterState(state);
            }
        }

        public void SetState(T state)
        {
            SetState(state, false);
        }

        protected virtual void LeaveState(T state)
        {
            if (OnLeaveState != null)
            {
                OnLeaveState(state);
            }
        }

        protected virtual void EnterState(T state)
        {
            if (OnEnterState != null)
            {
                OnEnterState(state);
            }
        }

        protected virtual void StateChange(T oldState, T newState)
        {
            if (OnStateChange != null)
            {
                OnStateChange(oldState, newState);
            }
        }
    }
}