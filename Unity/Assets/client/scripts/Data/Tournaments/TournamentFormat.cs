using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WeakReference = Data.WeakReference;

[Serializable]
public class TournamentStage
{
	[SerializeField] 
	private TournamentSeeding _seeding;
	
	[SerializeField] 
	private StageFormat _format;

	[SerializeField]
	private TournamentResult _result;

	public StageState GenerateState(int participants)
	{
		return _format.GenerateState(participants);
	}
	
	public StageState GenerateState(StageState lastStage)
	{
		//TODO support multi stage tournaments
		throw new NotImplementedException();
	}
}

[CreateAssetMenu(menuName = "Tournaments/TournamentFormat")]
public class TournamentFormat : ScriptableObject
{
	[SerializeField] private int _participants;
	
	[SerializeField]
	private List<TournamentStage> _stages;
	
	public TournamentState GenerateState()
	{
		TournamentState state = new TournamentState();

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
}