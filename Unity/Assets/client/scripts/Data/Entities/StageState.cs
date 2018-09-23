using System.Collections.Generic;
using System.Text.RegularExpressions;

public class StageState
{
    public RoundState Entrance { get; set; }
    public List<RoundState> Rounds { get; set; }
    
    public StageState()
    {
        Rounds = new List<RoundState>();
    }

    public ResultState GetResult()
    {
        if (Rounds.Count > 0)
            return Rounds[Rounds.Count - 1].Result;
        
        return null;
    }
}

public class MatchState
{
    public Result ParticipantA { get; set; }
    public Result ParticipantB { get; set; }

    public MatchState()
    {
        ParticipantA = new Result();
        ParticipantB = new Result();
    }
}

public class RoundState
{
    public List<MatchState> Matches { get; set; }
    public ResultState Result { get; set; }

    public RoundState()
    {
        Result = new ResultState();
        Matches = new List<MatchState>();
    }
}

public class ResultState
{
    public List<Result> Winners { get; set; }
    public List<Result> Losers { get;set;}
    
    public ResultState()
    {
        Winners = new List<Result>();
        Losers = new List<Result>();
    }
}

public class Result
{
    public Competitor Participant { get; set; }
}

