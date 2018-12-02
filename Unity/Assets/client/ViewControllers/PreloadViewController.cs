using PandeaGames.Views.ViewControllers;
using UnityEngine;
using PandeaGames;
using PandeaGames.Views;

namespace JunkyardDogs
{
    public enum PreloadViewStates
    {
        Splash,
        Loading,
        PreloadComplete
    }
    
    public class PreloadSplashState : SplashState<PreloadViewStates>
    {
        protected override void OnSplashComplete()
        {
            _fsm.SetState(PreloadViewStates.Loading);
        }
    }

    public class PreloadState : AbstractLoadingState<PreloadViewStates>
    {
        protected override void OnLoadComplete()
        {
            _fsm.SetState(PreloadViewStates.PreloadComplete);
        }
    }
    
    public class PreloadCompleteState : AbstractViewControllerState<PreloadViewStates>
    {
    }
    
    public class PreloadViewController : AbstractViewControllerFsm<PreloadViewStates>
    {
        public PreloadViewController()
        {
            SetViewStateController<PreloadSplashState>(PreloadViewStates.Splash);
            SetViewStateController<PreloadState>(PreloadViewStates.Loading);
            SetViewStateController<PreloadCompleteState>(PreloadViewStates.PreloadComplete);
        }

        public override void Initialize(IViewController parent)
        {
            base.Initialize(parent);
            SetInitialState(PreloadViewStates.Splash);
        }
   
        protected override IView CreateView()
        {
            return new JunkyardPreloadView();
        }
    }
}