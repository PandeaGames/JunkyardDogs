using PandeaGames;
using PandeaGames.Views.ViewControllers;
using PandeaGames.Views;

namespace JunkyardDogs
{
    public enum JunkyardDogsStates
    {
        EnterGame, 
        ChooseNationality, 
        Hub,
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
            if (_viewModel.UserData.Competitor.Nationality == null || _viewModel.UserData.Competitor.Nationality.Data == null)
            {
                _fsm.SetState(JunkyardDogsStates.ChooseNationality);
            }
            else
            {
                _fsm.SetState(JunkyardDogsStates.Hub);
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
                _fsm.SetState(JunkyardDogsStates.Hub);
                Game.Instance.GetService<JunkyardUserService>().Save();
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

                if (!tournamentViewModel.State.IsComplete())
                {
                    if (tournamentViewModel.State.IsUserKnockedOut())
                    {
                        userModel.UserData.Tournaments.CompleteTournament(tournamentViewModel.State.Uid);
                    }
                    else
                    {
                        userModel.UserData.Tournaments.UpdateTournament(tournamentViewModel.State);
                    }
                }
                
                Game.Instance.GetService<JunkyardUserService>().Save();
                _fsm.SetState(JunkyardDogsStates.Hub);
            }
        }
    }

    public class JunkuardHubViewState : AbstractViewControllerState<JunkyardDogsStates>
    {
        private WorldMapViewModel _viewModel;
        
        public JunkuardHubViewState() :base()
        {
            _viewModel = Game.Instance.GetViewModel<WorldMapViewModel>(0);
        }

        public override void LeaveState(JunkyardDogsStates to)
        {
            base.LeaveState(to);
            _viewModel.OnPlayTournament -= OnPlayTournament;
        }
        
        public override void EnterState(JunkyardDogsStates @from)
        {
            base.EnterState(@from);
            _fsm.GetView().GetWindow().RemoveCurrentScreen();
            _viewModel.OnPlayTournament += OnPlayTournament;
        }
        
        private void OnPlayTournament(TournamentState.TournamentStatus status)
        {
            var vm = Game.Instance.GetViewModel<WorldMapViewModel>(0);

            Game.Instance.GetViewModel<JunkyardUserViewModel>(0).UserData.Tournaments
                .TryGetTournamentMeta(status.Tournament.Uid, out TournamentMetaState metaState);
            TournamentViewModel tournamentViewModel = Game.Instance.GetViewModel<TournamentViewModel>(0);
            tournamentViewModel.User = Game.Instance.GetViewModel<JunkyardUserViewModel>(0).UserData;

            tournamentViewModel.Seed = metaState.GetHashCode();
            tournamentViewModel.Tournament = status.Tournament.TournamentReference;
            _fsm.SetState(JunkyardDogsStates.Tournament);
        }

        protected override IViewController GetViewController()
        {
            return new HubViewController();
        }
    }
    
    public class JunkyardDogsViewController : AbstractViewControllerFsm<JunkyardDogsStates>
    {
        public JunkyardDogsViewController()
        {
            SetViewStateController<EnterGameState>(JunkyardDogsStates.EnterGame);
            SetViewStateController<ChooseNationalityState>(JunkyardDogsStates.ChooseNationality);
            SetViewStateController<JunkuardHubViewState>(JunkyardDogsStates.Hub);
            SetViewStateController<WorldMapState>(JunkyardDogsStates.WorldMap);
            SetViewStateController<JunkyardState>(JunkyardDogsStates.Junkyard);
            SetViewStateController<JunkyardTournamentState>(JunkyardDogsStates.Tournament);
            SetInitialState(JunkyardDogsStates.EnterGame);
        }
                
        protected override IView CreateView()
        {
            return new JunkyardGameContainer();
        }
    }
}