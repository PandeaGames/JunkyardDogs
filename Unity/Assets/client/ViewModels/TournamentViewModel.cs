using System;
using JetBrains.Annotations;
using JunkyardDogs.Simulation;
using PandeaGames.ViewModels;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

namespace JunkyardDogs
{
    /*private WeakReference _tournament;
    private JunkyardUserService _userService;
    private Engagement _engagement;
    private TournamentState.TournamentStatus _tournamentStatus;
    private MatchController _matchController;
    private TournamentState _tournamentState;
    private RoundState _roundState;*/
    
    public class TournamentViewModel : AbstractViewModel, ILoadableObject
    {
        public JunkyardUser User;
        public bool RunFullTournament;
        public WeakReference Tournament;

        public TournamentState State
        {
            get { return _state; }
        }

        private Tournament _tournament;
        private TournamentState _state;
        private bool _isLoaded;

        public bool IsLoaded()
        {
            return _isLoaded;
        }

        public void LoadAsync(LoadSuccess onComplete, LoadError onError)
        {
            Tournament.LoadAsync(() =>
            {
                _tournament = Tournament.Asset as Tournament;
                _state = _tournament.GenerateState();
                //TODO: Get State from user data if it exists

                if (_state.RequiresParticipants())
                {
                    ParticipantDataUtils.GenerateParticipantsAsync(_tournament.Participants, (participants) =>
                    {
                        _state.FillWithParticipants(participants);
                        onComplete();
                    }, () => onError(new LoadException("Failed to load participants")));
                }
                else
                {
                    onComplete();
                }
            }, onError);
        }

        public UserParticipant GetUserParticipantForSelection()
        {
            UserParticipant output = null;
            
            foreach (Participant participant in _state.GetParticipants())
            {
                output = participant as UserParticipant;
                if (output != null && output.Bot == null)
                {
                    break;
                }

                output = null;
            }

            return output;
        }

        public void GetCurrentEngagement(Action<Engagement> onComplete, Action onError)
        {   
            TournamentState.TournamentStatus status = _state.GetStatus();
            MatchState match = status.Match;
            Engagement engagement = new Engagement();
            engagement.SetTimeLimit(300);

            match.ParticipantA.Participant.GetTeam(User, blueTeam =>
            {
                engagement.BlueCombatent = blueTeam.Bot;
                match.ParticipantB.Participant.GetTeam(User, redTeam =>
                {
                    engagement.RedCombatent = redTeam.Bot;
                    onComplete(engagement);
                }, onError);
            }, onError);
        }
    }
}