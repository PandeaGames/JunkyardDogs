using System;
using System.Collections.Generic;
using Data;
using PandeaGames.Utils;
using UnityEngine;

[Serializable]
public class TournamentMetaState
{
    [SerializeField] 
    private TournamentState _tournamentState;

    [SerializeField]
    private DateTime _lastCompleted;

    [SerializeField] 
    private int _completions;
    
    public TournamentState TournamentState { get => _tournamentState;
        set => _tournamentState = value;
    }
    public DateTime LastCompleted { get => _lastCompleted; set => _lastCompleted = value; }
    public int Completions { get => _completions;
        set => _completions = value;
    }

    public void CompleteTournament()
    {
        TournamentState = null;
        LastCompleted = DateTime.UtcNow;
        Completions++;
    }

    public bool CanPlay(Tournament tournament)
    {
        if (TournamentState == null)
        {
            return (DateTime.UtcNow - LastCompleted).TotalSeconds > tournament.SeasonDelaySeconds;
        }
        else
        {
            return !TournamentState.IsComplete();
        }
    }

    public bool HasState()
    {
        return TournamentState != null;
    }
}

public static class TournamentMetaStateUtils
{
    public static float GetPercentageUntilSeasonBegin(Tournament tournament, TournamentMetaState meta)
    {
        return Math.Min(1, (float)(DateTime.UtcNow - meta.LastCompleted).TotalSeconds / (float)tournament.SeasonDelaySeconds);
    }
}

[Serializable]
public class TournamentMetaStateKvP : PandeaGames.Utils.KeyValuePair<string, TournamentMetaState>
{
    
}

[Serializable]
public class TournamentMetaStatesDictionary : SerializableDictionary<string, TournamentMetaState, TournamentMetaStateKvP>
{
    
}

[Serializable]
public class Tournaments
{
    [SerializeField]
    private TournamentMetaStatesDictionary _tournamentStates;
    
    public TournamentMetaStatesDictionary TournamentStates {get { return _tournamentStates; } set { _tournamentStates = value; }}

    public Tournaments()
    {
        TournamentStates = new TournamentMetaStatesDictionary();
    }
    
    public void UpdateTournament(TournamentState state)
    {
        TournamentMetaState meta = null;
        TryGetTournamentMeta(state.TournamentReference.ID, out meta);
        meta.TournamentState = state;
    }

    public void TryGetTournament(string guid, out TournamentState state)
    {
        TournamentMetaState tournamentMetaState = TournamentStates.GetValue(guid);

        if (tournamentMetaState != null)
        {
            state = tournamentMetaState.TournamentState;
        }
        else
        {
            state = null;
        }
    }
    
    public void TryGetTournament(Tournament tournament, out TournamentState state)
    {
        TryGetTournament(tournament.Guid, out state);
    }
    
    public void TryGetTournamentMeta(string guid, out TournamentMetaState state)
    {
        state = TournamentStates.GetValue(guid);

        if (state == null)
        {
            state = new TournamentMetaState();
            TournamentStates.AddValue(guid, state);
        }
    }
    
    public void TryGetTournamentMeta(Tournament tournament, out TournamentMetaState state)
    {
        TryGetTournamentMeta(tournament.Guid, out state);
    }
    
    public void CompleteTournament(string guid)
    {
        TournamentMetaState tournamentMetaState = TournamentStates.GetValue(guid);

        if(tournamentMetaState != null)
            tournamentMetaState.CompleteTournament();
    }

    public void ClearTournamentStatus(string guid)
    {
        if(TournamentStates.Contains(guid))
            TournamentStates.Remove(guid);
    }
}
