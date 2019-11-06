using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

public class TournamentMetaState
{
    public TournamentState TournamentState { get; set; }
    public DateTime LastCompleted { get; set; }
    public int Completions { get; set; }

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
public class TournamentMetaStatesDictionary : Dictionary<string, TournamentMetaState>
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
        TournamentMetaState tournamentMetaState = null;
        TournamentStates.TryGetValue(guid, out tournamentMetaState);

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
        TournamentStates.TryGetValue(guid, out state);

        if (state == null)
        {
            state = new TournamentMetaState();
            TournamentStates.Add(guid, state);
        }
    }
    
    public void TryGetTournamentMeta(Tournament tournament, out TournamentMetaState state)
    {
        TryGetTournamentMeta(tournament.Guid, out state);
    }
    
    public void CompleteTournament(string guid)
    {
        TournamentMetaState tournamentMetaState = null;
        TournamentStates.TryGetValue(guid, out tournamentMetaState);

        if(tournamentMetaState != null)
            tournamentMetaState.CompleteTournament();
    }

    public void ClearTournamentStatus(string guid)
    {
        if(TournamentStates.ContainsKey(guid))
            TournamentStates.Remove(guid);
    }
}
