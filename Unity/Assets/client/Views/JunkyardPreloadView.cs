using UnityEngine;
using PandeaGames.Views;
using PandeaGames;
using PandeaGames.Views.Screens;

public class JunkyardPreloadView : AbstractUnityView
{
    private const string ViewPath = "Prefabs/JunkyardPreloadView";
    
    private GameObject _unityView;
    private GameObject _unityViewPrefab;

    public override void Show()
    {
        _unityView = GameObject.Instantiate(_unityViewPrefab);
        _window = _unityView.GetComponentInChildren<WindowView>();
    }
    
    public override void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
    {
        ResourceRequest request = Resources.LoadAsync<GameObject>(ViewPath);
        request.completed += (async) =>
        {
            _unityViewPrefab = request.asset as GameObject;
            onLoadSuccess();
        };
    }
}