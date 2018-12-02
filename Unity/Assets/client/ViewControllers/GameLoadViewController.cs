using System.Collections;
using PandeaGames.Views.ViewControllers;
using AssetBundles;
using PandeaGames;

namespace JunkyardDogs
{
    public enum LoadingStates
    {
        AssetBundles,
        StaticData,
        UserData
    }

    public class LoadAssetBundlesState : AbstractViewControllerState<LoadingStates>
    {
        private AssetBundleLoadAssetOperation _loadOperation;
        
        public override void EnterState(LoadingStates from)
        {
            _loadOperation = AssetBundleManager.Initialize();
        }

        public override void UpdateState()
        {
            if (_loadOperation == null || _loadOperation.IsDone())
            {
                _fsm.SetState(LoadingStates.UserData);
            }
        }
    }
    
    public class LoadStaticState : AbstractViewControllerState<LoadingStates>
    {
        public override void EnterState(LoadingStates from)
        {
            JunkyardStaticDataLoader loader = new JunkyardStaticDataLoader();
            loader.LoadAsync(() => _fsm.SetState(LoadingStates.UserData), (e) => { throw e; });
        }
    }
    
    public class LoadUserDataState : AbstractViewControllerState<LoadingStates>
    {
        public override void EnterState(LoadingStates @from)
        {
            Game.Instance.GetViewModel<JunkyardUserViewModel>(0).UserData = Game.Instance.GetService<JunkyardUserService>().Load();
        }
    }
    
    public class GameLoadViewController : AbstractViewControllerFsm<LoadingStates>
    {
        public GameLoadViewController()
        {
            SetViewStateController<LoadAssetBundlesState>(LoadingStates.AssetBundles);
            SetViewStateController<LoadStaticState>(LoadingStates.StaticData);
            SetViewStateController<LoadUserDataState>(LoadingStates.UserData);
            
            SetInitialState(LoadingStates.AssetBundles);
        }
    }
}