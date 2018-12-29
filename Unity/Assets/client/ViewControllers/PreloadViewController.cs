using AssetBundles;
using PandeaGames.Views.ViewControllers;
using UnityEngine;
using PandeaGames;
using PandeaGames.Views;

namespace JunkyardDogs
{
    public enum PreloadViewStates
    {
        Splash,
        AssetBundleLoad,
        Loading,
        PreloadComplete
    }
    
    public class PreloadSplashState : SplashState<PreloadViewStates>
    {
        protected override void OnSplashComplete()
        {
            _fsm.SetState(PreloadViewStates.AssetBundleLoad);
        }
    }
    
    public class PreloadAssetBundlesState : AbstractViewControllerState<PreloadViewStates>
    {
        private AssetBundleLoadAssetOperation _loadOperation;
        
        public override void EnterState(PreloadViewStates from)
        {
            string _url = "";
            #if UNITY_EDITOR
            _url = "file:///";
            #endif
            _url += Application.streamingAssetsPath + "/AssetBundles/";
            Debug.Log("URL: "+_url);
            AssetBundleManager.SetSourceAssetBundleURL(_url);
            
            _loadOperation = AssetBundleManager.Initialize();
        }

        public override void UpdateState()
        {
            if (_loadOperation == null || _loadOperation.IsDone())
            {
                _fsm.SetState(PreloadViewStates.Loading);
            }
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
            SetViewStateController<PreloadAssetBundlesState>(PreloadViewStates.AssetBundleLoad);
            SetViewStateController<PreloadState>(PreloadViewStates.Loading);
            SetViewStateController<PreloadCompleteState>(PreloadViewStates.PreloadComplete);
            
            SetInitialState(PreloadViewStates.Splash);
        }
   
        protected override IView CreateView()
        {
            return new JunkyardPreloadView();
        }
    }
}