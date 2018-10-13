using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using WeakReference = Data.WeakReference;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation;
using UnityEngine.SceneManagement;
using EditorSceneManager = UnityEditor.SceneManagement.EditorSceneManager;

public class TournamentController : MonoBehaviour 
{
	[SerializeField] 
	private ServiceManager _serviceManager;
	
	private WeakReference _tournament;
	private WeakReference _state;
	private List<WeakReference> _participants;
	
	private JunkyardUserService _userService;
	private SimulationService _simulationService;
	private Engagement _engagement;
	
	public void RunTournament(TournamentState tournamentState, WeakReference state)
	{
		_userService = _serviceManager.GetService<JunkyardUserService>();
		_simulationService = _serviceManager.GetService<SimulationService>();

		JunkyardUser user = _userService.User;
		
		_state = state;
		
		StartTournament(tournamentState);
	}
	
	public void RunTournament(WeakReference tournament, List<WeakReference> participants, WeakReference state)
	{
		_userService = _serviceManager.GetService<JunkyardUserService>();
		_simulationService = _serviceManager.GetService<SimulationService>();

		JunkyardUser user = _userService.User;
		
		_tournament = tournament;
		_participants = participants;
		_state = state;
		
		_tournament.LoadAsync<Tournament>(TournamentLoaded, OnError);
	}
	
	private void TournamentLoaded(Tournament tournament, WeakReference reference)
	{
		TournamentState state =  tournament.GenerateState();
        
		ParticipantDataUtils.GenerateParticipantsAsync(_participants, (participants) =>
		{
			state.FillWithParticipants(participants);

			TournamentState.TournamentStatus status = state.GetStatus();

			StartCoroutine(LoadMatchSceneCoroutine(status));
			
			

		}, OnError);
	}

	private IEnumerator LoadMatchSceneCoroutine(TournamentState.TournamentStatus status)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("client/scenes/gameScenes/Match", LoadSceneMode.Additive);

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}

		MatchController matchController = FindObjectOfType<MatchController>();
			
		matchController.RunMatch(status.Match, OnMatchComplete, _state);
	}

	private void OnMatchComplete()
	{
		
	}

	private void StartTournament(TournamentState tournamentState)
	{
		
	}	

	private void OnError()
	{
        
	}

	private void PrepareForBattle(Bot bot)
	{
		bot.Agent.States.ForEach((state) => { state.StateWeakReference = _state; });
	}

	private IEnumerator EndOfBattleCoroutine()
	{
		while (_engagement.Outcome == null)
		{
			yield return 0;
		}
        
		if (_engagement.Outcome.Winner == _engagement.RedCombatent)
		{
			Debug.Log("WINNER: RED");
		}
		else
		{
			Debug.Log("WINNER: Blue");
		}
	}
}
