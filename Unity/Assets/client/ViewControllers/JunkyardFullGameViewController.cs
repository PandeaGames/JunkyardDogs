using PandeaGames.Views.ViewControllers;
using UnityEngine;
using PandeaGames;
using PandeaGames.Views;

namespace JunkyardDogs
{
    public enum JunkyardGameStates
    {
        Splash,
        Loading,
        JunkyardDogs
    }
    
    public class SplashState : AbstractViewControllerState<JunkyardGameStates>
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

            if (Time.time - _startTicks > 2)
            {
                _fsm.SetState(JunkyardGameStates.Loading);
            }
        }
    }

    public class LoadingState : AbstractViewControllerState<JunkyardGameStates>
    {
        private JunkyardUserViewModel _viewModel = null;
        private JunkyardStaticDataLoader loader;
        
        public override void Initialize(AbstractViewControllerFsm<JunkyardGameStates> fsm)
        {
            base.Initialize(fsm);
            _viewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
        }

        protected override IViewController GetViewController()
        {
            return new GameLoadViewController();
        }

        public override void EnterState(JunkyardGameStates @from)
        {
            loader = new JunkyardStaticDataLoader();
            loader.LoadAsync(() => {  }, (e) =>
            {
                Debug.LogError(e);
            });

            _viewModel.SetUserData(Game.Instance.GetService<JunkyardUserService>().Load());
        }

        public override void UpdateState()
        {
            if (_viewModel.UserData != null && loader.IsLoaded())
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
            SetViewStateController<SplashState>(JunkyardGameStates.Splash);
            SetViewStateController<LoadingState>(JunkyardGameStates.Loading);
            SetViewStateController<JunkyardDogsState>(JunkyardGameStates.JunkyardDogs);
        }

        public override void Initialize(IViewController parent)
        {
            base.Initialize(parent);
            
            SetInitialState(JunkyardGameStates.Splash);
        }
   
        protected override IView CreateView()
        {
            return new JunkyardGameContainer();
        }
    }
}