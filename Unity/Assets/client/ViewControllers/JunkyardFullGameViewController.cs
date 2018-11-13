using PandeaGames.Views.ViewControllers;
using UnityEngine;
using PandeaGames;

namespace JunkyardDogs
{
    public enum JunkyardGameStates
    {
        Splash,
        Loading,
        JunkyardDogs
    }
    
    public class InitialState : AbstractViewControllerState<JunkyardGameStates>
    {
        private float _startTicks;
        
        public virtual void Initialize(AbstractViewControllerFsm<JunkyardGameStates> fsm)
        {
            base.Initialize(fsm);
        }

        public override void EnterState(JunkyardGameStates from)
        {
            _startTicks = Time.time;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (Time.time - _startTicks > 5)
            {
                _fsm.SetState(JunkyardGameStates.Loading);
            }
        }
    }

    public class LoadingState : AbstractViewControllerState<JunkyardGameStates>
    {
        private JunkyardUserViewModel _viewModel = null;
        
        public override void Initialize(AbstractViewControllerFsm<JunkyardGameStates> fsm)
        {
            base.Initialize(fsm);
            _viewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
        }

        protected override IViewController GetViewController()
        {
            return new GameLoadViewController(_fsm);
        }

        public override void UpdateState()
        {
            if (_viewModel.UserData != null)
            {
                _fsm.SetState(JunkyardGameStates.JunkyardDogs);
            }
            
            base.UpdateState();
        }
    }
    
    public class JunkyardDogsState : AbstractViewControllerState<JunkyardGameStates>
    {
        public override void Initialize(AbstractViewControllerFsm<JunkyardGameStates> fsm)
        {
            base.Initialize(fsm);
        }

        protected override IViewController GetViewController()
        {
            return new JunkyardDogsViewController();
        }
    }
    
    public class JunkyardFullGameViewController : AbstractViewControllerFsm<JunkyardGameStates>
    {
        public JunkyardFullGameViewController()
        {
            SetViewStateController<InitialState>(JunkyardGameStates.Splash);
            SetViewStateController<LoadingState>(JunkyardGameStates.Loading);
            SetViewStateController<JunkyardDogsState>(JunkyardGameStates.JunkyardDogs);
            SetInitialState(JunkyardGameStates.Splash);
        }
    }
}