using PandeaGames.Views.ViewControllers;
using System;
using UnityEngine;
using PandeaGames;

namespace JunkyardDogs
{
    public abstract class AbstractLoadingState<TStates> : AbstractViewControllerState<TStates>
    {
        private JunkyardUserViewModel _viewModel = null;
        private JunkyardStaticDataLoader loader;
        
        public override void Initialize(AbstractViewControllerFsm<TStates> fsm)
        {
            base.Initialize(fsm);
            _viewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
        }

        protected override IViewController GetViewController()
        {
            return new GameLoadViewController();
        }

        public override void EnterState(TStates @from)
        {
            loader = new JunkyardStaticDataLoader();
            loader.LoadAsync(() => { Debug.Log("LOADING DONE"); }, (e) =>
            {
                Debug.LogError(e);
            });

            _viewModel.SetUserData(Game.Instance.GetService<JunkyardUserService>().Load());
        }

        public override void UpdateState()
        {
            bool isLoadingCompleted = _viewModel.UserData != null && loader.IsLoaded;
            
            if (isLoadingCompleted)
            {
                OnLoadComplete();
            }
            
            base.UpdateState();
        }
        
        protected abstract void OnLoadComplete();
    }
}