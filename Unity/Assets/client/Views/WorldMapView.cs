using I2.Loc;
using JunkyardDogs;
using JunkyardDogs.Data;
using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames;
using PandeaGames.Runtime.Dialogs.ViewModels;
using PandeaGames.Views;
using PandeaGames.Views.ViewControllers;
using UnityEngine;

public class WorldMapView : AbstractUnityView
{
    private WorldMapViewModel _worldMapViewModel;
    private GameObject _worldView;
    private EarthNavigation navigation;
    private JunkyardUserViewModel _userViewModel;

    public override void InitializeView(IViewController controller)
    {
        base.InitializeView(controller);
        _worldMapViewModel = Game.Instance.GetViewModel<WorldMapViewModel>(0);
        _worldMapViewModel.OnTournamentTapped += OnTournamentTapped;
        _worldView = GameObject.Instantiate(Game.Instance.GetStaticDataPovider<GameStaticDataProvider>().GameDataStaticData.WorldView);

        _worldView.SetActive(false);
        navigation = _worldView.GetComponent<EarthNavigation>();
        _userViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
    }
    
   /* public override void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
    {
        //_worldView = GameObject.Instantiate(Game.Instance.GetStaticDataPovider<GameStaticDataProvider>().GameDataStaticData.WorldView);
        onLoadSuccess();
    }*/

    public override void Show()
    {
        FindWindow().LaunchScreen("WorldMapHUD");
        _worldView.SetActive(true);
        Game.Instance.GetViewModel<CameraViewModel>(0).Focus(navigation.cameraAgent);
        //_userViewModel.OnAscend += Ascend;
        _worldMapViewModel.OnTryAscend += OnTryAscend;
    }

    private void OnTryAscend(Nationality nationality)
    {
        uint level = nationality == null ? _userViewModel.UserData.Experience.Level:_userViewModel.UserData.Experience.GetLevel(nationality);
        
        MessageDialogViewModel vm = new MessageDialogViewModel();
        vm.SetOptions(
            new MessageDialogViewModel.Option("UI.ok"),
            string.Format(LocalizationManager.GetTranslation("UI.dialog.level_up_msg"), (level + 1).ToString()));

        if (nationality == null)
        {
            vm.OnClose += dialog => Game.Instance.GetViewModel<JunkyardUserViewModel>(0).Ascend();
        }
        else
        {
            vm.OnClose += dialog => Game.Instance.GetViewModel<JunkyardUserViewModel>(0).Ascend(nationality);
        }
        
        FindServiceManager().GetService<DialogService>().DisplayDialog<MessageDialog>(vm);
    }

    public override void Destroy()
    {
        base.Destroy();
        _worldMapViewModel.OnTournamentTapped -= OnTournamentTapped;
        GameObject.Destroy(navigation.gameObject);
        _worldMapViewModel.OnTryAscend -= OnTryAscend;
    }

    private void OnTournamentTapped(TournamentStaticDataReference obj)
    {
        EventDialogViewModel vm = Game.Instance.GetViewModel<EventDialogViewModel>();
        vm.TournamentReference = obj;
        vm.OnPlayTournament += _worldMapViewModel.PlayTournament;
        FindServiceManager().GetService<DialogService>().DisplayDialog<EventDialog>(vm);
    }
}
