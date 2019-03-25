using System.Collections.Generic;
using UnityEngine;
using System;
using JunkyardDogs.Data.Balance;

[CreateAssetMenu(menuName = "Tournaments/Formats/EliminationFormat")]
public class EliminationFormat : StageFormat, IStaticDataBalance<EliminationFormatBalanceObject>
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
			GenerateRound(rounds);
		}
	}
	
	private void GenerateRound(List<RoundState> rounds)
	{
		RoundState lastRound = rounds[rounds.Count - 1];
		EliminationRoundState round = new EliminationRoundState();
		rounds.Add(round);

		for (int i = 0; i < lastRound.Matches.Count; i+=2)
		{
			MatchState match = new MatchState();
			match.ParticipantA = lastRound.Matches[i].Winner;
			match.ParticipantB = lastRound.Matches[i + 1].Winner;
			round.AddMatch(match);
		}
		
		if (round.Matches.Count > 1)
		{
			GenerateRound(rounds);
		}
	}
    
	public override StageState GenerateState(StageState lastStage)
	{
		//TODO support multi stage tournaments
		throw new NotImplementedException();
	}

	public void ApplyBalance(EliminationFormatBalanceObject balance)
	{
		name = balance.name;
		_eliminations = balance.eliminations;
		_groups = balance.groups;
	}

	public EliminationFormatBalanceObject GetBalance()
	{
		EliminationFormatBalanceObject balance = new EliminationFormatBalanceObject();

		balance.name = name;
		balance.groups = _groups;
		balance.eliminations = _eliminations;

		return balance;
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
	}
}

public class EliminationResultState : ResultState
{
	public EliminationResultState() :base()
	{
	}
}