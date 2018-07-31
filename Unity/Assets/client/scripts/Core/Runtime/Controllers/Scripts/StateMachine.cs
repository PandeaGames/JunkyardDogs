using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<T> : MonoBehaviour where T:IConvertible
{
    protected T _currentState;
    protected bool _canChangeState = true;

    protected virtual void Start()
    {
        SetState(default(T), true);
    }

    private void SetState(T state, bool isInitialState)
    {
        if (_canChangeState && (!state.Equals(_currentState) || isInitialState))
        {
            LeaveState(_currentState);
            _currentState = state;
            EnterState(state);
        }
    }

    protected void SetState(T state)
    {
        SetState(state, false);
    }

    protected abstract void EnterState(T state);
    protected abstract void LeaveState(T state);
}
