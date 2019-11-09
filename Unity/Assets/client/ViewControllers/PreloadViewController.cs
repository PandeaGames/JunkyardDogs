using AssetBundles;
using PandeaGames.Views.ViewControllers;
using UnityEngine;
using PandeaGames;
using PandeaGames.Views;

#if UNITY_EDITOR
using UnityEditor;
#endif


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
            string _url = "file:///";
            #if UNITY_ANDROID
            _url = string.Empty;
            #endif
            _url += Application.streamingAssetsPath + "/";
            Debug.Log("URL: "+_url);
            AssetBundleManager.SetSourceAssetBundleURL(_url);
            
            _loadOperation = AssetBundleManager.Initialize();
        }

        private bool wasCompleted = false;

        private bool isSimulatedBundles
        {
            get
            {
#if UNITY_EDITOR
                return EditorPrefs.GetBool("SimulateAssetBundles");
                #endif
                return false;
            }
        }

        public override void UpdateState()
        {
            if (wasCompleted)
            {
                _fsm.SetState(PreloadViewStates.Loading);
            }
            
            #if UNITY_EDITOR
            if ((_loadOperation != null && _loadOperation.IsDone()) || isSimulatedBundles)
                #else
                if (_loadOperation != null && _loadOperation.IsDone())
#endif
            
            {
                wasCompleted = true;
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