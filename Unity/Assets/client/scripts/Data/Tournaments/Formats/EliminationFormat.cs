using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(menuName = "Tournaments/Formats/EliminationFormat")]
public class EliminationFormat : StageFormat
{
	[Range(1, 10)][SerializeField]
	private int _eliminations = 1;
	
	public override StageState GenerateState(int participants)
	{
		EliminationStageState stageState = new EliminationStageState();
		
		//TODO implement more than single elimination
		GenerateRound(stageState.Rounds, participants);

		return stageState;
	}

	private void GenerateRound(List<RoundState> rounds, int participants)
	{
		//TODO: Support group play
		EliminationRoundState round = new EliminationRoundState();
		rounds.Add(round);

		for (int i = 0; i < participants / 2; i++)
		{
			MatchState match = new MatchState();
			round.AddMatch(match);
		}

		if (round.Matches.Count > 1)
		{
			GenerateRound(rounds, round.Result);
		}
	}
	
	private void GenerateRound(List<RoundState> rounds, ResultState perviousResult)
	{
		EliminationRoundState round = new EliminationRoundState();
		rounds.Add(round);
		EliminationResultState eliminationResults = perviousResult as EliminationResultState;

		for (int i = 0; i < eliminationResults.Winners.Count; i += 2)
		{
			MatchState match = new MatchState();
			match.ParticipantA = eliminationResults.Winners[i];
			match.ParticipantB = eliminationResults.Winners[i+1];
			round.AddMatch(match);
		}
		
		if (round.Matches.Count > 1)
		{
			GenerateRound(rounds, round.Result);
		}
	}
    
	public override StageState GenerateState(ResultState perviousResult)
	{
		EliminationStageState stateState = new EliminationStageState();
		GenerateRound(stateState.Rounds, perviousResult);
		return stateState;
	}
}

public class EliminationStageState : StageState
{
	public EliminationStageState() : base()
	{
		
	}
}

public class EliminationRoundState : RoundState
{
	public EliminationRoundState() : base()
	{
		Result = new EliminationResultState();
	}

	public void AddMatch(MatchState match)
	{
		Matches.Add(match);
		
		(Result as EliminationResultState).Winners.Add(new Result());
		(Result as EliminationResultState).Losers.Add(new Result());
	}
}

public class EliminationResultState : ResultState
{
	public EliminationResultState()
	{
	}
}