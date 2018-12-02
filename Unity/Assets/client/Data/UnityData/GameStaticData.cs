using UnityEngine;
using Data;
using JunkyardDogs.Simulation.Behavior;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[CreateAssetMenu(menuName = "StaticData/GameStaticData")]
public class GameStaticData : ScriptableObject, ILoadableObject
{
    [SerializeField, WeakReference(typeof(NationList))] 
    private WeakReference _nations;
    public NationList Nations
    {
        get { return _nations.Asset as NationList; }
    }
    
    [SerializeField][WeakReference(typeof(ActionList))] 
    private WeakReference _actionList;
    public ActionList ActionList
    {
        get { return _actionList.Asset as ActionList; }
    }
    
    public WeakReference ActionListRef
    {
        get { return _actionList; }
    }
    

    private bool _isLoaded;

    public bool IsLoaded()
    {
        return _isLoaded;
    }

    public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
    {
        Loader loader = new Loader();
        loader.AppendProvider(_nations);
        loader.AppendProvider(_actionList);
        loader.LoadAsync(() =>
        {
            Loader secondaryLoader = new Loader();
            secondaryLoader.AppendProvider(Nations as ILoadableObject);
            secondaryLoader.AppendProvider(ActionList as ILoadableObject);

            secondaryLoader.LoadAsync(() =>
            {
                _isLoaded = true;
                onLoadSuccess();
            }, onLoadError);
            
        }, onLoadError);
    }
}
