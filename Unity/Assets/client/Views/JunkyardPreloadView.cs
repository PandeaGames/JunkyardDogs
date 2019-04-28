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
        _unityView = GameObject.Instantiate(_unityViewPrefab, FindParentTransform());
        _window = _unityView.GetComponentInChildren<WindowView>();
        FindWindow().LaunchScreen("preloader");
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

    public override void Destroy()
    {
        base.Destroy();
        GameObject.Destroy(_unityView);
    }
}