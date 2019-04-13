using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JunkyardDogs.Data;
using JunkyardDogs.Data.Balance;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[Serializable]
public class TournamentStage
{
	/*[SerializeField] 
	private TournamentSeeding _seeding;*/
	
	[SerializeField, StageFormatStaticDataReference] 
	public StageFormatStaticDataReference format;

	/*[SerializeField]
	private TournamentResult _result;*/

	public StageState GenerateState(int participants)
	{
		return format.Data.GenerateState(participants);
	}
	
	public StageState GenerateState(StageState lastStage)
	{
		//TODO support multi stage tournaments
		throw new NotImplementedException();
	}
}

[Serializable]
public struct StagesContainer
{
	public List<TournamentStage> stages;
}

[CreateAssetMenu(menuName = "Tournaments/TournamentFormat")]
public class TournamentFormat : ScriptableObject, IStaticDataBalance<TournamentFormatBalanceObject>
{
	[SerializeField] private int _participants;
	
	[SerializeField]
	private List<TournamentStage> _stages;
	
	public TournamentState GenerateState(string uid)
	{
		TournamentState state = new TournamentState(uid);

		for (int i = 0; i < _stages.Count; i++)
		{
			TournamentStage stage = _stages[i];
			StageState stageState = null;
			
			bool isFirstStage = i == 0;
			
			if (isFirstStage)
			{
				stageState = stage.GenerateState(_participants);
			}
			else
			{
				stageState = stage.GenerateState(state.StageStates[state.StageStates.Count - 1]);
			}
			
			state.StageStates.Add(stageState);
		}
		
		return state;
	}

	public void ApplyBalance(TournamentFormatBalanceObject balance)
	{
		name = balance.name;
		_participants = balance.participants;
		//StagesContainer stagesContainer = JsonUtility.FromJson<StagesContainer>(balance.stages);
		//_stages = stagesContainer.stages;
	}

	public TournamentFormatBalanceObject GetBalance()
	{
		TournamentFormatBalanceObject balance = new TournamentFormatBalanceObject();

		StagesContainer stagesContainer = new StagesContainer();
		stagesContainer.stages = _stages;

		_stages = new List<TournamentStage>();
		
		ImportStage(_stages, balance.stage_01);
		ImportStage(_stages, balance.stage_02);
		ImportStage(_stages, balance.stage_03);
		ImportStage(_stages, balance.stage_04);
		ImportStage(_stages, balance.stage_05);
		
		balance.name = name;
		balance.participants = _participants;
		
		return balance;
	}

	private void ImportStage(List<TournamentStage> tournamentStage, string stageId)
	{
		if (!string.IsNullOrEmpty(stageId))
		{
			TournamentStage stage = new TournamentStage();
			stage.format = new StageFormatStaticDataReference();
			stage.format.ID = stageId;
			tournamentStage.Add(stage);
		}
	}
}