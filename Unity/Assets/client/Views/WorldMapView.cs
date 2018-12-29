using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames;
using PandeaGames.Data.WeakReferences;
using PandeaGames.Views;
using PandeaGames.Views.ViewControllers;

public class WorldMapView : AbstractUnityView
{
    private WorldMapViewModel _worldMapViewModel;

    public override void InitializeView(IViewController controller)
    {
        base.InitializeView(controller);
        _worldMapViewModel = Game.Instance.GetViewModel<WorldMapViewModel>(0);
        var vm = Game.Instance.GetViewModel<WorldMapViewModel>(0); 
        vm.OnTournamentTapped += OnTournamentTapped;
    }

    public override void Show()
    {
        FindWindow().LaunchScreen("worldMap");
    }
    
    private void OnTournamentTapped(WeakReference obj)
    {
        EventDialogViewModel vm = Game.Instance.GetViewModel<EventDialogViewModel>();
        vm.TournamentReference = obj;
        vm.OnPlayTournament += _worldMapViewModel.PlayTournament;
        FindServiceManager().GetService<DialogService>().DisplayDialog<EventDialog>(vm);
    }
}
