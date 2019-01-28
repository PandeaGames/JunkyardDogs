using Data;
using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;


[CreateAssetMenu(menuName = "StaticData/ViewStaticData")]
public class ViewStaticData : ScriptableObject, ILoadableObject
{
    [SerializeField, WeakReference(typeof(GameObject))] 
    private WeakReference _simpleWindowPrefab;
    public GameObject SimpleWindowPrefab
    {
        get { return _simpleWindowPrefab.Asset as GameObject; }
    }
    
    [SerializeField, WeakReference(typeof(GameObject))] 
    private WeakReference _junkyardGameContainer;
    public GameObject JunkyardGameContainer
    {
        get { return _junkyardGameContainer.Asset as GameObject; }
    }
    
    [SerializeField, WeakReference(typeof(GameObject))] 
    private WeakReference _hubView;
    public GameObject HubView
    {
        get { return _hubView.Asset as GameObject; }
    }

    private bool _isLoaded;

    public bool IsLoaded()
    {
        return _isLoaded;
    }

    public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
    {
        Loader loader = new Loader();
        loader.AppendProvider(_simpleWindowPrefab);
        loader.AppendProvider(_junkyardGameContainer);
        loader.AppendProvider(_hubView);
        loader.LoadAsync(onLoadSuccess, onLoadError);
    }
}
