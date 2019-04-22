using System;
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
                Debug.Log("[AbstractViewControllerState]["+from+"] EnterState.ShowView Start");
                _viewController.Initialize(_fsm);
                _viewController.ShowView();
                Debug.Log("[AbstractViewControllerState]["+from+"] EnterState.ShowView Done");
            }
        }
        
        public virtual void LeaveState(TEnum to)
        {
            if (_viewController != null)
            {
                Debug.Log("[AbstractViewControllerState]["+to+"] LeaveState.RemoveView Start");
                RemoveView();
                Debug.Log("[AbstractViewControllerState]["+to+"] LeaveState.RemoveView Done");
            }
        }

        public virtual void RemoveView()
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

        public override void RemoveView()
        {
            AbstractViewControllerState<TEnum> vc = null;

            _states.TryGetValue(_currentState, out vc);

            if (vc != null)
            {
                vc.RemoveView();
            }
            
            base.RemoveView();
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
            //TaskProvider.Instance.DelayedAction(() =>
            //{
                Debug.Log("[AbstractViewControllerFsm]["+state+"] SetState Start");

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
                
                if (OnEnterState != null)
                    OnEnterState(state);
                if(oldController != null && !isInitialState)
                    oldController.LeaveState(state);

                _currentState = state;
                newController.EnterState(_currentState);
                Debug.Log("[AbstractViewControllerFsm]["+state+"] SetState Done");
            //});
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
}

