using System.Collections.Generic;
using Data;

public class TournamentState
{
    public WeakReference TournamentReference { get; set; }
    public List<StageState> StageStates { get; set; }

    public TournamentState()
    {
        StageStates = new List<StageState>();
    }
}

public static class TournamentStateUtils
{
}