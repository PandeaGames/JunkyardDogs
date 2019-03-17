using JunkyardDogs;
using PandeaGames;
using PandeaGames.Views;
using PandeaGames.Views.ViewControllers;

public enum HubStates
{
    MainMap = 0, 
    Garage = 1,
    Junkyard = 2
}

public class HubViewController : AbstractViewControllerFsm<HubStates>
{
    public class MainMapState : AbstractViewControllerState<HubStates>
    {
        protected override IViewController GetViewController()
        {
            return new WorldMapViewController();
        }
    }
    
    public class GarageState : AbstractViewControllerState<HubStates>
    {
        protected override IViewController GetViewController()
        {
            return new GarageViewController();
        }
    }
    
    public class JunkyardState : AbstractViewControllerState<HubStates>
    {
        protected override IViewController GetViewController()
        {
            return new JunkyardViewController();
        }
    }
    
    public HubViewController()
    {
        SetViewStateController<MainMapState>(HubStates.MainMap);
        SetViewStateController<GarageState>(HubStates.Garage);
        SetViewStateController<JunkyardState>(HubStates.Junkyard);
        SetInitialState(HubStates.MainMap);
        
        Game.Instance.GetViewModel<HubViewModel>(0).OnEnterState += OnEnterHubState;
        
    }

    private void OnEnterHubState(HubStates state)
    {
        SetState(state);
        Game.Instance.GetService<JunkyardUserService>().Save();
    }

    protected override void OnAfterShow()
    {
        base.OnAfterShow();
    }

    protected override IView CreateView()
    {
        return new HubView();
    }
}