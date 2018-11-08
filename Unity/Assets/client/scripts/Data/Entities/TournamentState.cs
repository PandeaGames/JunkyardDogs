using System.Collections.Generic;
using Data;
using UnityEngine.UI.Extensions;

public class TournamentState
{
    public struct TournamentStatus
    {
        public TournamentState Tournament;
        public StageState Stage;
        public RoundState Round;
        public MatchState Match;

        public bool IsComplete()
        {
            return Tournament == null ? false : Tournament.IsComplete();
        }
    }
    
    public WeakReference TournamentReference { get; set; }
    public List<StageState> StageStates { get; set; }
    public string Uid  { get; set; }

    public TournamentState(string uid)
    {
        StageStates = new List<StageState>();
        Uid = uid;
    }

    public bool FillWithParticipants(List<Participant> participants)
    {
        StageState firstStage = StageStates[0];
        RoundState firstRound = firstStage.Rounds[0];

        firstRound.FillWithParticipantsRandom(participants);
        
        return false;
    }

    public StageState GetCurrentStage()
    {
        StageState output = null;
        
        foreach (StageState stageState in StageStates)
        {
            output = stageState;
            if (!stageState.IsComplete())
            {
                break;
            }
        }

        return output;
    }

    public bool IsComplete()
    {
        return GetCurrentStage().IsComplete();
    }

    public TournamentStatus GetStatus()
    {
        TournamentStatus status = default(TournamentStatus);

        status.Tournament = this;
        status.Stage = GetCurrentStage();
        status.Round = status.Stage.GetCurrentRound();
        status.Match = status.Round.GetCurrentMatch();

        return status;
    }
}

public static class TournamentStateUtils
{
}