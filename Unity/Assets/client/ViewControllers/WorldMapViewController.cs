using JunkyardDogs;
using JunkyardDogs.scripts.Runtime.Dialogs;
using JunkyardDogs.Simulation;
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
        vm.SetData(data);
    }

    protected override IView CreateView()
    {
        return new WorldMapView();
    }
}
