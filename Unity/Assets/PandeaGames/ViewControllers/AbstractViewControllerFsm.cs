﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace PandeaGames.Views.ViewControllers
{
    public abstract class AbstractViewControllerState<TEnum>
    {
        protected AbstractViewControllerFsm<TEnum> _fsm;
        protected IViewController _viewController;

        public virtual void Initialize(AbstractViewControllerFsm<TEnum> fsm)
        {
            _fsm = fsm;
        }

        protected virtual IViewController GetViewController()
        {
            return null;
        }
        
        public virtual void EnterState(TEnum from)
        {
            _viewController = GetViewController();
            if (_viewController != null)
            {
                _viewController.Initialize(_fsm);
                _viewController.ShowView();
            }
           
        }
        
        public virtual void LeaveState(TEnum to)
        {
            if (_viewController != null)
            {
                _viewController.RemoveView();
            }
        }

        public virtual void UpdateState()
        {
            if (_viewController != null)
            {
                _viewController.Update();
            }
        }
    }
    
    public class AbstractViewControllerFsm<TEnum> : AbstractViewController
    {
        public event Action<TEnum> OnEnterState;
        
        private Dictionary<TEnum, AbstractViewControllerState<TEnum>> _states;
        private TEnum _currentState;
        private TEnum _initialState;
        private bool _isInInitialState;
        
        public AbstractViewControllerFsm()
        {
            _states = new Dictionary<TEnum, AbstractViewControllerState<TEnum>>();
        }

        protected void SetViewStateController<TState>(TEnum state) where TState : AbstractViewControllerState<TEnum>, new()
        {
            if (_states.ContainsKey(state))
            {
                _states.Remove(state);
            }
            
            AbstractViewControllerState<TEnum> controller = new TState();
            controller.Initialize(this);
            _states.Add(state, controller);
        }

        protected void SetInitialState(TEnum state)
        {
            _initialState = state;
            _isInInitialState = true; 
        }

        public override void ShowView()
        {
            base.ShowView();

            if (_isInInitialState)
            {
                SetState(_initialState, true);
            }
            else
            {
                SetState(_currentState);
            }
        }

        private void SetState(TEnum state, bool isInitialState)
        {
            TaskProvider.Instance.DelayedAction(() =>
            {
                if (state.Equals(_currentState) && !isInitialState)
                {
                    return;
                }
            
                if (!_states.ContainsKey(state))
                {
                    throw new NotImplementedException(string.Format("State '{0}' has not been implemented by this machine.", state));
                }

                AbstractViewControllerState<TEnum> newController = _states[state];
                AbstractViewControllerState<TEnum> oldController = null;

                if (_states.ContainsKey(_currentState))
                {
                    oldController = _states[_currentState];
                }

                Debug.LogFormat("[EnterState] {0}", state);
                newController.EnterState(_currentState);
                if (OnEnterState != null)
                    OnEnterState(state);
                if(oldController != null && !isInitialState)
                    oldController.LeaveState(state);

                _currentState = state;
            });
        }

        public void SetState(TEnum state)
        {
            SetState(state, false);
        }

        public override void Update()
        {
            AbstractViewControllerState<TEnum> controller = null;

            if (_states.ContainsKey(_currentState))
            {
                controller = _states[_currentState];
                controller.UpdateState();
            }
        }
    }
    
    public enum TestStates
    {
        State1, 
        State2
    }

    public class TestState1Controller : AbstractViewControllerState<TestStates>
    {
        protected override IViewController GetViewController()
        {
            return null;
        }
    }
    
    public class TestState2Controller : AbstractViewControllerState<TestStates>
    {
        protected override IViewController GetViewController()
        {
            return null;
        }
    }

    public class TestViewController : AbstractViewControllerFsm<TestStates>
    {
        public TestViewController()
        {
            SetViewStateController<TestState1Controller>(TestStates.State1);
            SetViewStateController<TestState2Controller>(TestStates.State2);
            SetInitialState(TestStates.State1);
        }
    }
}

