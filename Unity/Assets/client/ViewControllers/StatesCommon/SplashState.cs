using PandeaGames.Views.ViewControllers;
using System;
using UnityEngine;

namespace JunkyardDogs
{
    public abstract class SplashState<TStates>:AbstractViewControllerState<TStates>
    {
        private float _startTicks;
        
        public virtual void Initialize(AbstractViewControllerFsm<TStates> fsm)
        {
            base.Initialize(fsm);
        }

        public override void EnterState(TStates from)
        {
            _startTicks = Time.time;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (Time.time - _startTicks > 2)
            {
                OnSplashComplete();
            }
        }

        protected abstract void OnSplashComplete();
    }
}