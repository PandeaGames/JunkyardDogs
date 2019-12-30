using System;
using UnityEngine;
using Data;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using JunkyardDogs.Simulation.Behavior;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[Serializable]
public class GameConfigurationDataBalanceObject : IStaticDataBalanceObject
{
    public string name;
    public string nationalExpBreakpoints;
    public string expBreakpoints;
    public string statusBarCurrencyTag;
        
    public string GetDataUID()
    {
        return name;
    }
}

[CreateAssetMenu(menuName = "StaticData/GameStaticData")]
public class GameStaticData : ScriptableObject, ILoadableObject, IStaticDataBalance<GameConfigurationDataBalanceObject>
{
    [SerializeField]
    private SpriteFactory _nationFlagFactory;
    
    [SerializeField][WeakReference(typeof(ActionList))] 
    private WeakReference _actionList;
    public ActionList ActionList
    {
        get { return _actionList.Asset as ActionList; }
    }
    
    [SerializeField][WeakReference(typeof(GameObject))] 
    private WeakReference _worldView;
    public GameObject WorldView
    {
        get { return _worldView.Asset as GameObject; }
    }
    
    [SerializeField][WeakReference(typeof(GameObject))] 
    private WeakReference _junkyardView;
    public GameObject JunkyardView
    {
        get { return _junkyardView.Asset as GameObject; }
    }
    
    [SerializeField][WeakReference(typeof(JunkyardData))] 
    private WeakReference _junkyardTestData;
    public JunkyardData JunkyardTestData
    {
        get { return _junkyardTestData.Asset as JunkyardData; }
    }
    
    [SerializeField]
    private PrefabFactory _botPrefabFactory;
    public PrefabFactory BotPrefabFactory
    {
        get { return _botPrefabFactory; }
    }
    
    [SerializeField]
    private PrefabFactory _componentPrefabFactory;
    public PrefabFactory ComponentPrefabFactory
    {
        get { return _componentPrefabFactory; }
    }
    
    [SerializeField]
    private Material _lightboxKeyMaterial;
    public Material LightboxKeyMaterial
    {
        get { return _lightboxKeyMaterial; }
    }

    [SerializeField]
    private string _statusBarCurrencyTag;
    public string StatusBarCurrencyTag
    {
        get => _statusBarCurrencyTag;
    }
    
    
    public WeakReference ActionListRef
    {
        get { return _actionList; }
    }

    [BreakpointStaticDataReference]
    public BreakpointStaticDataReference NationalExpBreakpoints;
    [BreakpointStaticDataReference]
    public BreakpointStaticDataReference ExpBreakpoints;

    public bool IsLoaded { get; private set; }

    public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
    {
        Loader loader = new Loader();
        loader.AppendProvider(_actionList);
        loader.AppendProvider(_worldView);
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
    
    public void ApplyBalance(GameConfigurationDataBalanceObject balance)
    {
        NationalExpBreakpoints = new BreakpointStaticDataReference();
        ExpBreakpoints = new BreakpointStaticDataReference();
        _statusBarCurrencyTag = balance.statusBarCurrencyTag;

        NationalExpBreakpoints.ID = balance.nationalExpBreakpoints;
        ExpBreakpoints.ID = balance.expBreakpoints;
    }

    public GameConfigurationDataBalanceObject GetBalance()
    {
        GameConfigurationDataBalanceObject output = new GameConfigurationDataBalanceObject();
        output.nationalExpBreakpoints = NationalExpBreakpoints.ID;
        output.expBreakpoints = ExpBreakpoints.ID;
        return output;
    }
}
