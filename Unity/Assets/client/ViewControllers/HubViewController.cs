using JunkyardDogs;
using JunkyardDogs.Data;
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

    private JunkyardUserViewModel _userViewModel;
    
    public HubViewController()
    {
        _userViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
            
        SetViewStateController<MainMapState>(HubStates.MainMap);
        SetViewStateController<GarageState>(HubStates.Garage);
        SetViewStateController<JunkyardState>(HubStates.Junkyard);
        SetInitialState(HubStates.MainMap);
    }

    private void OnJunkyardTapped(JunkyardData junkyardData)
    {
        if (_userViewModel.UserData.Competitor.Inventory.Bots.Count > 0)
        {
            JunkyardStaticDataReference reference = new JunkyardStaticDataReference();
            reference.ID = junkyardData.ID;
            Game.Instance.GetViewModel<JunkyardUserViewModel>(0).UserData.Junkard = reference;
            Game.Instance.GetViewModel<HubViewModel>(0).SetState(HubStates.Junkyard);
        }
    }

    private void OnEnterHubState(HubStates state)
    {
        SetState(state);
        Game.Instance.GetService<JunkyardUserService>().Save();
    }

    protected override void OnAfterShow()
    {
        base.OnAfterShow();
        Game.Instance.GetViewModel<WorldMapViewModel>(0).OnJunkyardTapped += OnJunkyardTapped;
        Game.Instance.GetViewModel<HubViewModel>(0).OnEnterState += OnEnterHubState;
    }

    public override void RemoveView()
    {
        base.RemoveView();
        Game.Instance.GetViewModel<HubViewModel>(0).OnEnterState -= OnEnterHubState;
        Game.Instance.GetViewModel<WorldMapViewModel>(0).OnJunkyardTapped -= OnJunkyardTapped;
    }

    protected override IView CreateView()
    {
        return new HubView();
    }
}