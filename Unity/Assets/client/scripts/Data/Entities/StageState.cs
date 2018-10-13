using System.Collections.Generic;
using System.Text.RegularExpressions;

public class StageState
{
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
    
    public RoundState GetCurrentRound()
    {
        RoundState output = null;
        foreach (RoundState round in Rounds)
        {
            output = round;
            if (!round.IsComplete())
            {
                break;
            }
        }
        
        return output;
    }

    public bool IsComplete()
    {
        return GetCurrentRound().IsComplete();
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
    
    public bool FillWithParticipantsRandom(List<Participant> participants)
    {
        if (participants.Count < Matches.Count / 2)
        {
            return false;
        }

        for (int i = 0; i < Matches.Count; i++)
        {
            MatchState match = Matches[i];

            match.ParticipantA.Participant = participants[i * 2];
            match.ParticipantB.Participant = participants[(i * 2) + 1];
        }
        
        return true;
    }

    public bool IsComplete()
    {
        return Result.IsComplete();
    }
    
    public MatchState GetCurrentMatch()
    {
        int resultIndex = Result.CurrentResultIndex();

        if (resultIndex == -1)
        {
            return null;
        }
        
        return Matches[resultIndex];
    }
}

public class ResultState
{
    public List<Result> Winners { get; set; }
    public List<Result> Losers { get; set;}
    
    public ResultState()
    {
        Winners = new List<Result>();
        Losers = new List<Result>();
    }
    
    public bool IsComplete()
    {
        foreach (var winner in Winners)
        {
            if (!winner.HasResult())
            {
                return false;
            }
        }
        
        foreach (var loser in Losers)
        {
            if (!loser.HasResult())
            {
                return false;
            }
        }
        
        return true;
    }

    public int CurrentResultIndex()
    {
        for (int i = 0; i < Winners.Count; i++)
        {
            if (!Winners[i].HasResult())
            {
                return i;
            }
        }

        return Winners.Count - 1;
    }
}

public class Result
{
    public Participant Participant { get; set; }

    public bool HasResult()
    {
        return Participant != null;
    }
}

