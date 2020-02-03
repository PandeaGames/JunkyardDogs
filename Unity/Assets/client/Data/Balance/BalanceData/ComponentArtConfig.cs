using System;
using Data;
using PandeaGames;
using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[Serializable]
public class ComponentArtConfigData : ConfigListBalanceDataObj, ILoadableObject
{
    [SerializeField, WeakReference(typeof(GameObject))]
    private WeakReference _prefab;
    public GameObject Prefab
    {
        get => _prefab.Asset as GameObject;
    }
    
    public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadFailed)
    {
        LoaderGroup loaderGroup = new LoaderGroup();

        loaderGroup.AppendProvider(_prefab);
        
        loaderGroup.LoadAsync(() =>
        {
            IsLoaded = true;
            onLoadSuccess?.Invoke();
        }, onLoadFailed);
    }

    public bool IsLoaded { get; private set; }
}

[CreateAssetMenu(menuName = MENU_NAME), Serializable]
public class ComponentArtConfig : AbstractConfigListBalanceData<ComponentArtConfigData>
{
    private const string MENU_NAME =  BalanceDataUtilites.BALANCE_MENU_FOLDER + "ComponentArtConfig";
    
    
}