using System;
using System.Collections.Generic;
using Data;
using JunkyardDogs.Data;
using UnityEngine;
using UnityEngine.UI.Extensions;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[Serializable]
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

        public bool IsUserKnockedOut()
        {
            return  Tournament == null ? false : Tournament.IsUserKnockedOut();
        }
    }

    [SerializeField, TournamentStaticDataReference]
    private TournamentStaticDataReference _tournamentReference;
    [SerializeField]
    private List<StageState> _stageStates;
    [SerializeField]
    private string _uid;
    [SerializeField]
    private DateTime _lastMatch;
    
    public TournamentStaticDataReference TournamentReference { get=>_tournamentReference; set=> _tournamentReference = value; }
    public List<StageState> StageStates { get => _stageStates; set => _stageStates = value; }
    public string Uid  { get => _uid; set => _uid = value; }
    public DateTime LastMatch { get => _lastMatch; set => _lastMatch = value; }

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
    
    public bool IsUserKnockedOut()
    {
        return GetCurrentStage().IsUserKnockedOut();
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
    public static bool IsRoundReady(Tournament tournament, TournamentState state)
    {
        return (DateTime.UtcNow - state.LastMatch).TotalSeconds >= tournament.RoundPaceSeconds;
    }
    
    public static float GetRoundReadyPercentage(Tournament tournament, TournamentState state)
    {
        return Math.Min(1, (float)(DateTime.UtcNow - state.LastMatch).TotalSeconds / (float)tournament.RoundPaceSeconds);
    }
}