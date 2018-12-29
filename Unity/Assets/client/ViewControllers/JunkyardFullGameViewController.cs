using PandeaGames.Views.ViewControllers;
using UnityEngine;
using PandeaGames;
using PandeaGames.Views;

namespace JunkyardDogs
{
    public enum JunkyardGameStates
    {
        Preload,
        JunkyardDogs
    }
    
    public class PreloadJunkyardState : AbstractViewControllerState<JunkyardGameStates>
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
                _fsm.SetState(JunkyardGameStates.JunkyardDogs);
            }
        }
    }
    
    public class JunkyardDogsState : AbstractViewControllerState<JunkyardGameStates>
    {
        protected override IViewController GetViewController()
        {
            ContainerViewController vc = new ContainerViewController(
                new JunkyardDogsGameViewController(),
                new JunkyardDogsViewController()
            );

            return vc;
        }
    }
    
    public class JunkyardFullGameViewController : AbstractViewControllerFsm<JunkyardGameStates>
    {
        public JunkyardFullGameViewController()
        {
            SetViewStateController<PreloadJunkyardState>(JunkyardGameStates.Preload);
            SetViewStateController<JunkyardDogsState>(JunkyardGameStates.JunkyardDogs);
            
            SetInitialState(JunkyardGameStates.Preload);
        }
    }
}