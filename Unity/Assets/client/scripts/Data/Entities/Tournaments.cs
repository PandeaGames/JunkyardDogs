using System.Collections.Generic;
using Data;

public class Tournaments
{
    public Dictionary<string, TournamentState> TournamentStates;

    public Tournaments()
    {
        TournamentStates = new Dictionary<string, TournamentState>();
    }

    public void TryGetTournament(string path, out TournamentState state)
    {
        TournamentStates.TryGetValue(path, out state);
    }
    
    public void TryGetTournament(WeakReference reference, out TournamentState state)
    {
        TryGetTournament(reference.Path, out state);
    }
}
