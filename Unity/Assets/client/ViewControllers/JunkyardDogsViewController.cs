using PandeaGames;
using PandeaGames.Views.ViewControllers;

namespace JunkyardDogs
{
    public enum JunkyardDogsStates
    {
        EnterGame, 
        ChooseNationality, 
        WorldMap
    }

    public class EnterGameState : AbstractViewControllerState<JunkyardDogsStates>
    {
        private JunkyardUserViewModel _viewModel;
        
        public EnterGameState() : base()
        {
            _viewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
        }

        public override void UpdateState()
        {
            if (_viewModel.UserData.Competitor.Nationality.Asset == null)
            {
                _fsm.SetState(JunkyardDogsStates.ChooseNationality);
            }
            else
            {
                _fsm.SetState(JunkyardDogsStates.WorldMap);
            }
        }
    }
    
    public class ChooseNationalityState : AbstractViewControllerState<JunkyardDogsStates>
    {
        
    }
    
    public class WorldMapState : AbstractViewControllerState<JunkyardDogsStates>
    {
        
    }
    
    public class JunkyardDogsViewController : AbstractViewControllerFsm<JunkyardDogsStates>
    {
        public JunkyardDogsViewController()
        {
            SetViewStateController<EnterGameState>(JunkyardDogsStates.EnterGame);
            SetViewStateController<ChooseNationalityState>(JunkyardDogsStates.ChooseNationality);
            SetViewStateController<WorldMapState>(JunkyardDogsStates.WorldMap);
            
            SetInitialState(JunkyardDogsStates.EnterGame);
        }
    }
}