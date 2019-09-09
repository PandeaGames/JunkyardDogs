using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using JunkyardDogs.Data;
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
    
    public class TournamentViewModel : AbstractViewModel
    {
        public JunkyardUser User;
        public bool RunFullTournament;
        public TournamentStaticDataReference Tournament;

        public TournamentState State
        {
            get { return _state; }
        }

        private Tournament _tournament;
        private TournamentState _state;
        public int Seed;

       private TournamentState GetState()
       {
           _tournament = Tournament.Data;
            
           User.Tournaments.TryGetTournament(_tournament, out _state);
                
           if(_state == null)
               _state = _tournament.GenerateState();
           
           if (_state.RequiresParticipants())
           {
               List<Participant> participants = new List<Participant>();

               foreach (ParticipantStaticDataReference participantReference in _tournament.Participants)
               {
                   participants.Add(participantReference.Data.GetParticipant());
               }
               _state.FillWithParticipants(participants);
           }

           return _state;
       }

        public UserParticipant GetUserParticipantForSelection()
        {
            UserParticipant output = null;
            
            foreach (Participant participant in GetState().GetParticipants())
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
            TournamentState.TournamentStatus status = GetState().GetStatus();
            MatchState match = status.Match;
            Engagement engagement = new Engagement();
            engagement.Seed = Seed;
            engagement.SetTimeLimit(30);

            engagement.BlueCombatent = match.ParticipantA.Participant.GetTeam(User).Bot;
            engagement.RedCombatent = match.ParticipantB.Participant.GetTeam(User).Bot;
            
            onComplete(engagement);
        }
    }
}