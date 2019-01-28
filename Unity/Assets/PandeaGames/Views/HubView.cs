using PandeaGames;
using PandeaGames.Views;
using PandeaGames.Views.Screens;
using UnityEngine;

public class HubView : AbstractUnityView
{
    private RectTransform _rt;
    private Transform _transform;
    
    private GameObject _view;
    public void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
    {
        onLoadSuccess();
    }
    
    public override Transform GetTransform()
    {
        return _transform;
    }

    public override RectTransform GetRectTransform()
    {
        return _rt;
    }

    public override void Show()
    {
        _view = GameObject.Instantiate(Game.Instance.GetStaticDataPovider<ViewStaticDataProvider>().ViewStaticData
            .HubView, FindParentTransform());
        _transform = _view.GetComponent<Transform>();
        _window = _view.GetComponentInChildren<WindowView>();
    }

    public override void Destroy()
    {
        base.Destroy();
        GameObject.Destroy(_view);
    }
}
