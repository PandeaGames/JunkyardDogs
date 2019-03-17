using PandeaGames;
using PandeaGames.Views.ViewControllers;

namespace JunkyardDogs
{
    public enum FullMatchStates
    {
        Preload, 
        Match
    }

    public class PreloadMatchState:AbstractViewControllerState<FullMatchStates>
    {
        private MatchViewModel _matchViewModel;
        
        public PreloadMatchState()
        {
            _matchViewModel = Game.Instance.GetViewModel<MatchViewModel>(0);
        }

        public override void EnterState(FullMatchStates @from)
        {
            base.EnterState(@from);
            _fsm.SetState(FullMatchStates.Match);
        }
    }
    
    public class MatchViewState:AbstractViewControllerState<FullMatchStates>
    {
        protected override IViewController GetViewController()
        {
            return new MatchViewController();
        }
    }
    
    public class FullMatchViewController : AbstractViewControllerFsm<FullMatchStates>
    {
        public FullMatchViewController()
        {
            SetViewStateController<PreloadMatchState>(FullMatchStates.Preload);
            SetViewStateController<MatchViewState>(FullMatchStates.Match);
            
            SetInitialState(FullMatchStates.Preload);
        }
    }
}