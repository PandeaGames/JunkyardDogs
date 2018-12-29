using System;
using System.Collections.Generic;
using Data;
using UnityEngine.UI.Extensions;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

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
    public DateTime LastMatch { get; set; }

    public TournamentState(string uid)
    {
        StageStates = new List<StageState>();
        Uid = uid;
    }
    
    public TournamentState()
    {
    }

    public bool FillWithParticipants(List<Participant> participants)
    {
        RoundState firstRound = GetFirstRound();
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

    public StageState GetFirstStage()
    {
        return StageStates[0];
    }
    
    public RoundState GetFirstRound()
    {
        return GetFirstStage().Rounds[0];
    }

    public bool RequiresParticipants()
    {
        return !GetFirstRound().HasParticipants();
    }

    public IEnumerable<Participant> GetParticipants()
    {
        return GetFirstStage().GetParticipants();
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