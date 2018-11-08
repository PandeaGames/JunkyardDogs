using System.Collections.Generic;
using Data;

public class Tournaments
{
    public Dictionary<string, TournamentState> TournamentStates;

    public Tournaments()
    {
        TournamentStates = new Dictionary<string, TournamentState>();
    }
    
    public void UpdateTournament(TournamentState state)
    {
        if (!TournamentStates.ContainsKey(state.Uid))
        {
            TournamentStates.Add(state.Uid, state);
        }
    }

    public void TryGetTournament(string guid, out TournamentState state)
    {
        TournamentStates.TryGetValue(guid, out state);
    }
    
    public void TryGetTournament(Tournament tournament, out TournamentState state)
    {
        TryGetTournament(tournament.Guid, out state);
    }
}
