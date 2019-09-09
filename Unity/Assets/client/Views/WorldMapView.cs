using JunkyardDogs.Data;
using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames;
using PandeaGames.Data.WeakReferences;
using PandeaGames.Views;
using PandeaGames.Views.ViewControllers;
using UnityEngine;

public class WorldMapView : AbstractUnityView
{
    private WorldMapViewModel _worldMapViewModel;
    private GameObject _worldView;
    private EarthNavigation navigation;

    public override void InitializeView(IViewController controller)
    {
        base.InitializeView(controller);
        _worldMapViewModel = Game.Instance.GetViewModel<WorldMapViewModel>(0);
        _worldMapViewModel.OnTournamentTapped += OnTournamentTapped;
        _worldView = GameObject.Instantiate(Game.Instance.GetStaticDataPovider<GameStaticDataProvider>().GameDataStaticData.WorldView);

        _worldView.SetActive(false);
        navigation = _worldView.GetComponent<EarthNavigation>();
    }
    
   /* public override void LoadAsync(LoadSuccess onLoadSuccess, LoadError onLoadError)
    {
        //_worldView = GameObject.Instantiate(Game.Instance.GetStaticDataPovider<GameStaticDataProvider>().GameDataStaticData.WorldView);
        onLoadSuccess();
    }*/

    public override void Show()
    {
        FindWindow().LaunchScreen("worldMap");
        _worldView.SetActive(true);
        Game.Instance.GetViewModel<CameraViewModel>(0).Focus(navigation.cameraAgent);
    }

    public override void Destroy()
    {
        base.Destroy();
        _worldMapViewModel.OnTournamentTapped -= OnTournamentTapped;
    }

    private void OnTournamentTapped(TournamentStaticDataReference obj)
    {
        EventDialogViewModel vm = Game.Instance.GetViewModel<EventDialogViewModel>();
        vm.TournamentReference = obj;
        vm.OnPlayTournament += _worldMapViewModel.PlayTournament;
        FindServiceManager().GetService<DialogService>().DisplayDialog<EventDialog>(vm);
    }
}
