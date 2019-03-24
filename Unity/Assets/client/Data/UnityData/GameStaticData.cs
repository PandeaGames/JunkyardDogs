using UnityEngine;
using Data;
using JunkyardDogs.Simulation.Behavior;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[CreateAssetMenu(menuName = "StaticData/GameStaticData")]
public class GameStaticData : ScriptableObject, ILoadableObject
{
    [SerializeField][WeakReference(typeof(ActionList))] 
    private WeakReference _actionList;
    public ActionList ActionList
    {
        get { return _actionList.Asset as ActionList; }
    }
    
    [SerializeField]
    private PrefabFactory _botPrefabFactory;
    public PrefabFactory BotPrefabFactory
    {
        get { return _botPrefabFactory; }
    }
    
    public WeakReference ActionListRef
    {
        get { return _actionList; }
    }
    
    public bool IsLoaded { get; private set; }

    public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
    {
        Loader loader = new Loader();
        loader.AppendProvider(_actionList);
        loader.LoadAsync(() =>
        {
            Loader secondaryLoader = new Loader();
            secondaryLoader.AppendProvider(ActionList as ILoadableObject);

            secondaryLoader.LoadAsync(() =>
            {
                IsLoaded = true;
                onLoadSuccess();
            }, onLoadError);
            
        }, onLoadError);
    }
}
