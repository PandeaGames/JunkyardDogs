using PandeaGames.Views.ViewControllers;
using PandeaGames;
using PandeaGames.Data.WeakReferences;
using PandeaGames.Views;

public class WorldMapViewController : AbstractViewController
{
    protected override void OnBeforeShow()
    {
        JunkyardUser user = Game.Instance.GetService<JunkyardUserService>().User;
          
        WorldMapViewModel.Data data = new WorldMapViewModel.Data(
            user
        );

        var vm = Game.Instance.GetViewModel<WorldMapViewModel>(0);
            
        vm.OnTournamentTapped += OnTournamentTapped;
        
        vm.SetData(data);
    }

    private void OnTournamentTapped(WeakReference obj)
    {
       // _view.GetServiceManager().GetService<DialogService>().DisplayDialog<EventDialog>();
    }

    protected override IView CreateView()
    {
        return new WorldMapView();
    }
}
