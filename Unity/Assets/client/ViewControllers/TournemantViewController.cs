using System;
using System.Runtime.Serialization.Formatters;
using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames;
using PandeaGames.Views.ViewControllers;

namespace JunkyardDogs
{
    public enum TournamentStates
    {
        Preload,
        FillParticipants,
        ChooseUserParticipant,
        PrepareMatch,
        RunMatch,
        MatchComplete
    }

    public class TournamentPreload:AbstractViewControllerState<TournamentStates>
    {
        private TournamentViewModel _viewModel;
        private JunkyardUserViewModel _userViewModel;

        public TournamentPreload()
        {
            _viewModel = Game.Instance.GetViewModel<TournamentViewModel>(0);
            _userViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
            _viewModel.User = _userViewModel.UserData;
        }

        public override void EnterState(TournamentStates @from)
        {
            base.EnterState(@from);
            _fsm.SetState(TournamentStates.FillParticipants);
        }
    }
    
    public class TournamentFillParticipants:AbstractViewControllerState<TournamentStates>
    {
        private TournamentViewModel _viewModel;
        
        public TournamentFillParticipants()
        {
            _viewModel = Game.Instance.GetViewModel<TournamentViewModel>(0);
        }
        
        public override void EnterState(TournamentStates @from)
        {
            base.EnterState(@from);

            UserParticipant userParticipant = _viewModel.GetUserParticipantForSelection();

            if (userParticipant == null)
            {
                _fsm.SetState(TournamentStates.PrepareMatch);
            }
            else
            {
                _fsm.SetState(TournamentStates.ChooseUserParticipant);
            }
        }
    }
    
    public class TournamentChooseUserParticipantState:AbstractViewControllerState<TournamentStates>
    {
        private TournamentViewModel _tournamentViewModel;
        private ChooseBotFromInventoryViewModel _viewModel;
        private JunkyardUserViewModel _userViewModel;
        
        public TournamentChooseUserParticipantState()
        {
            _viewModel = Game.Instance.GetViewModel<ChooseBotFromInventoryViewModel>(0);
            _userViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
            _tournamentViewModel = Game.Instance.GetViewModel<TournamentViewModel>(0);
        }
        
        protected override IViewController GetViewController()
        {            
            return new ChooseBotFromInventoryViewController();
        }

        public override void EnterState(TournamentStates @from)
        {
            _viewModel.User = _userViewModel.UserData;
            _viewModel.UserParticipant = _tournamentViewModel.GetUserParticipantForSelection();
            _viewModel.OnBotChosen += OnBotChosen;
            
            base.EnterState(@from);
        }

        public override void LeaveState(TournamentStates to)
        {
            base.LeaveState(to);

            _userViewModel.UserData.Tournaments.UpdateTournament(_tournamentViewModel.State);
            _viewModel.OnBotChosen -= OnBotChosen;
        }

        private void OnBotChosen()
        {
            _fsm.SetState(TournamentStates.FillParticipants);
        }
    }
    
    public class TournamentPrepareMatch:AbstractViewControllerState<TournamentStates>
    {
        private MatchViewModel _matchViewModel;
        private TournamentViewModel _tournamentViewModel;

        public TournamentPrepareMatch()
        {
            _matchViewModel = Game.Instance.GetViewModel<MatchViewModel>(0);
            _tournamentViewModel = Game.Instance.GetViewModel<TournamentViewModel>(0);
        }
        
        public override void EnterState(TournamentStates @from)
        {
            base.EnterState(@from);
            TournamentState.TournamentStatus status = _tournamentViewModel.State.GetStatus();
            
            _tournamentViewModel.GetCurrentEngagement(engagement =>
            {
                _matchViewModel.Engagement = engagement;
                _fsm.SetState(TournamentStates.RunMatch);
            }, () => { });
        }
    }
    
    public class TournamentRunMatch:AbstractViewControllerState<TournamentStates>
    {
        private MatchViewModel _matchViewModel;
        private TournamentViewModel _tournamentViewModel;
        
        public TournamentRunMatch()
        {
            _matchViewModel = Game.Instance.GetViewModel<MatchViewModel>(0);
            _tournamentViewModel = Game.Instance.GetViewModel<TournamentViewModel>(0);
        }
        
        protected override IViewController GetViewController()
        {
            return new FullMatchViewController();
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (_matchViewModel.Engagement.Outcome != null)
            {
                TournamentState.TournamentStatus status = _tournamentViewModel.State.GetStatus();

                Participant winner =
                    _matchViewModel.Engagement.Outcome.Winner == _matchViewModel.Engagement.BlueCombatent
                        ? status.Match.ParticipantA.Participant
                        : status.Match.ParticipantB.Participant;
                
                Participant loser =
                    _matchViewModel.Engagement.Outcome.Winner == _matchViewModel.Engagement.BlueCombatent
                        ? status.Match.ParticipantB.Participant
                        : status.Match.ParticipantA.Participant;

                status.Match.Winner.Participant = winner;
                status.Match.Loser.Participant = loser;
                
                _tournamentViewModel.State.LastMatch = DateTime.UtcNow;
                
                _fsm.SetState(TournamentStates.MatchComplete);
            }
        }
    }
    
    public class TournamentMatchComplete:AbstractViewControllerState<TournamentStates>
    {
        
    }  
    
    public class TournemantViewController : AbstractViewControllerFsm<TournamentStates>
    {
        public TournemantViewController()
        {
            SetViewStateController<TournamentPreload>(TournamentStates.Preload);
            SetViewStateController<TournamentFillParticipants>(TournamentStates.FillParticipants);
            SetViewStateController<TournamentChooseUserParticipantState>(TournamentStates.ChooseUserParticipant);
            SetViewStateController<TournamentPrepareMatch>(TournamentStates.PrepareMatch);
            SetViewStateController<TournamentRunMatch>(TournamentStates.RunMatch);
            SetViewStateController<TournamentMatchComplete>(TournamentStates.MatchComplete);
            
            SetInitialState(TournamentStates.Preload);
        }
    }
}