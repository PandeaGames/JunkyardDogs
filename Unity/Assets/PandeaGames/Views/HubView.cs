using I2.Loc;
using JunkyardDogs;
using PandeaGames;
using PandeaGames.Runtime.Dialogs.ViewModels;
using PandeaGames.Views;
using PandeaGames.Views.Screens;
using UnityEngine;

public class HubView : AbstractUnityView
{
    private RectTransform _rt;
    private Transform _transform;
    private WorldMapViewModel _worldMapViewModel;
    private JunkyardUserViewModel _userViewModel;
    
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
        _worldMapViewModel = Game.Instance.GetViewModel<WorldMapViewModel>(0);
        _userViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
        _worldMapViewModel.OnJunkyardTapped += WorldMapViewModelOnJunkyardTapped;
    }

    private void WorldMapViewModelOnJunkyardTapped(JunkyardData obj)
    {
        if (_userViewModel.UserData.Competitor.Inventory.Bots.Count <= 0)
        {
            MessageDialogViewModel vm = new MessageDialogViewModel();
            vm.SetOptions(
                new MessageDialogViewModel.Option("UI.ok"),
                LocalizationManager.GetTranslation("UI.dialog.bot_required"));
        
            FindServiceManager().GetService<DialogService>().DisplayDialog<MessageDialog>(vm);
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        GameObject.Destroy(_view);
        _worldMapViewModel.OnJunkyardTapped -= WorldMapViewModelOnJunkyardTapped;
    }
}
