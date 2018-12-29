using PandeaGames;
using PandeaGames.Views.ViewControllers;
using PandeaGames.Views;

namespace JunkyardDogs
{
    public enum JunkyardDogsStates
    {
        EnterGame, 
        ChooseNationality, 
        WorldMap,
        Junkyard,
        Tournament,
        Garage
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
        protected override IViewController GetViewController()
        {
            var vc = new ChooseNationalityViewController();

            vc.OnNationChosen += () =>
            {
                _fsm.SetState(JunkyardDogsStates.WorldMap);
            };
            
            return vc;
        }
    }
    
    public class WorldMapState : AbstractViewControllerState<JunkyardDogsStates>
    {
        private WorldMapViewModel _viewModel;
        
        public WorldMapState()
        {
            _viewModel = Game.Instance.GetViewModel<WorldMapViewModel>(0);
            
        }
        
        protected override IViewController GetViewController()
        {
            return new WorldMapViewController();
        }

        public override void EnterState(JunkyardDogsStates @from)
        {
            base.EnterState(@from);
            _viewModel.OnJunkyardTapped += OnJunkyardTapped;
            _viewModel.OnPlayTournament += OnPlayTournament;
        }

        public override void LeaveState(JunkyardDogsStates to)
        {
            base.LeaveState(to);
            _viewModel.OnJunkyardTapped -= OnJunkyardTapped;
            _viewModel.OnPlayTournament -= OnPlayTournament;
        }

        private void OnJunkyardTapped()
        {
            _fsm.SetState(JunkyardDogsStates.Junkyard);
        }
        
        private void OnPlayTournament(TournamentState.TournamentStatus status)
        {
            var vm = Game.Instance.GetViewModel<WorldMapViewModel>(0);

            TournamentViewModel tournamentViewModel = Game.Instance.GetViewModel<TournamentViewModel>(0);
            tournamentViewModel.User = Game.Instance.GetViewModel<JunkyardUserViewModel>(0).UserData;
        
            tournamentViewModel.Tournament = status.Tournament.TournamentReference;
            _fsm.SetState(JunkyardDogsStates.Tournament);
        }
    }
    
    public class JunkyardState : AbstractViewControllerState<JunkyardDogsStates>
    {
        protected override IViewController GetViewController()
        {
            return new JunkyardViewController();
        }
    }
    
    public class JunkyardTournamentState : AbstractViewControllerState<JunkyardDogsStates>
    {
        
        protected override IViewController GetViewController()
        {
            TournemantViewController vc = new TournemantViewController();
            vc.OnEnterState += OnEnterState;
            
            return vc;
        }

        private void OnEnterState(TournamentStates obj)
        {
            if (obj == TournamentStates.MatchComplete)
            {
                JunkyardUserViewModel userModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
                TournamentViewModel tournamentViewModel = Game.Instance.GetViewModel<TournamentViewModel>(0);
                userModel.UserData.Tournaments.UpdateTournament(tournamentViewModel.State);
                Game.Instance.GetService<JunkyardUserService>().Save();
                _fsm.SetState(JunkyardDogsStates.WorldMap);
            }
        }
    }
    
    public class JunkyardDogsViewController : AbstractViewControllerFsm<JunkyardDogsStates>
    {
        public JunkyardDogsViewController()
        {
            SetViewStateController<EnterGameState>(JunkyardDogsStates.EnterGame);
            SetViewStateController<ChooseNationalityState>(JunkyardDogsStates.ChooseNationality);
            SetViewStateController<WorldMapState>(JunkyardDogsStates.WorldMap);
            SetViewStateController<JunkyardState>(JunkyardDogsStates.Junkyard);
            SetViewStateController<JunkyardTournamentState>(JunkyardDogsStates.Tournament);
            SetInitialState(JunkyardDogsStates.EnterGame);
        }
    }
}