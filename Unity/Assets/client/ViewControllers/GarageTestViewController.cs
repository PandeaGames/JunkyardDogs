using PandeaGames.Views.ViewControllers;

namespace JunkyardDogs
{
    public enum GarageTestViewStates
    {
        Preloading,
        Garage
    }
    
    public class GaragePreloadState : AbstractViewControllerState<GarageTestViewStates>
    {
        protected override IViewController GetViewController()
        {
            PreloadViewController vc = new PreloadViewController();
            vc.OnEnterState += PreloaderOnEnterState;
            return vc;
        }

        private void PreloaderOnEnterState(PreloadViewStates state)
        {
            if (state == PreloadViewStates.PreloadComplete)
            {
                _fsm.SetState(GarageTestViewStates.Garage);
            }
        }
    }
    
    public class GarageTestState : AbstractViewControllerState<GarageTestViewStates>
    {
        protected override IViewController GetViewController()
        {
            ContainerViewController vc = new ContainerViewController(
                new JunkyardDogsGameViewController(),
                new GarageViewController()
                );

            return vc;
        }
    }
    
    public class GarageTestViewController : AbstractViewControllerFsm<GarageTestViewStates>
    {
        public GarageTestViewController()
        {
            SetViewStateController<GaragePreloadState>(GarageTestViewStates.Preloading);
            SetViewStateController<GarageTestState>(GarageTestViewStates.Garage);
            
            SetInitialState(GarageTestViewStates.Preloading);
        }
    }
}