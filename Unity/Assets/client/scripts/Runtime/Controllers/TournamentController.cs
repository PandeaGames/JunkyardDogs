using System;
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
	private Engagement _engagement;
	private TournamentState.TournamentStatus _tournamentStatus;
	private MatchController _matchController;
	private TournamentState _tournamentState;
	
	public void RunTournament(TournamentState tournamentState, WeakReference state)
	{
		_userService = _serviceManager.GetService<JunkyardUserService>();

		JunkyardUser user = _userService.User;
		
		_state = state;

		_tournamentState = tournamentState;
		StartTournament(tournamentState);
	}
	
	public void RunTournament(WeakReference tournament, List<WeakReference> participants, WeakReference state)
	{
		_tournament = tournament;
		_participants = participants;
		_state = state;
		
		_tournament.LoadAsync<Tournament>(TournamentLoaded, OnError);
	}
	
	private void TournamentLoaded(Tournament tournament, WeakReference reference)
	{
		_tournamentState =  tournament.GenerateState();
        
		ParticipantDataUtils.GenerateParticipantsAsync(_participants, (participants) =>
			{
				StartCoroutine(CompleteParticipantsCoroutine(participants));
			}, OnError);
	}

	private IEnumerator CompleteParticipantsCoroutine(List<Participant> participants)
	{

		bool searchForUserBots = false;

		do
		{
			searchForUserBots = false;
			UserParticipant userParticipant = null;
			foreach (Participant participant in participants)
			{
				userParticipant = participant as UserParticipant;
				if (userParticipant != null && userParticipant.Bot == null)
				{
					break;
				}
			}

			if (userParticipant != null && userParticipant.Bot == null)
			{
				searchForUserBots = true;
				
				//TODO: load scene and controller that chooses a robot from the user's garage. 
				AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("client/scenes/gameScenes/ChooseBot", LoadSceneMode.Additive);

				// Wait until the asynchronous scene fully loads
				while (!asyncLoad.isDone)
				{
					yield return null;
				}
				
				ChooseUserBotController chooseController = FindObjectOfType<ChooseUserBotController>();
				bool hasError = false;
				Bot chosenBot = null;
				
				chooseController.ChooseBot(bot => { chosenBot = bot; }, () =>
				{
					hasError = true;
				}, () => { hasError = true; });

				yield return new WaitUntil(() => hasError || chosenBot != null);

				if (hasError)
				{
					OnError();
				}
				else
				{
					userParticipant.Bot = chosenBot;
				}

				Destroy(chooseController.gameObject);
			}

			yield return 0;

		} while (searchForUserBots);
		
		_tournamentState.FillWithParticipants(participants);
		RunTournament(_tournamentState, _state);
	}

	private IEnumerator UnloadMatchSceneCoroutine(Action onComplete)
	{
		AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync("client/scenes/gameScenes/Match");

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}

		onComplete();
	}

	private IEnumerator LoadMatchSceneCoroutine(TournamentState.TournamentStatus status)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("client/scenes/gameScenes/Match", LoadSceneMode.Additive);

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}

		_matchController = FindObjectOfType<MatchController>();
		_matchController.RunMatch(status.Match, OnMatchComplete, _state);
	}

	private void OnMatchComplete(MatchOutcome outcomed)
	{
		outcomed.Match.Winner.Participant = outcomed.Winner;
		outcomed.Match.Loser.Participant = outcomed.Loser;

		if (_tournamentState.IsComplete())
		{
			GameObject.Destroy(_matchController.gameObject);
			//StartCoroutine(UnloadMatchSceneCoroutine(() => { }));
		}
		else
		{
			GameObject.Destroy(_matchController.gameObject);
			StartTournament(_tournamentState);
			/*StartCoroutine(UnloadMatchSceneCoroutine(() =>
			{
				//StartTournament(_tournamentState);
			}));*/
		}
	}

	private void StartTournament(TournamentState tournamentState)
	{
		_tournamentStatus = tournamentState.GetStatus();

		if (_tournamentStatus.IsComplete())
		{
			Debug.Log("WINNER");
		}
		else
		{
			StartCoroutine(LoadMatchSceneCoroutine(_tournamentStatus));
		}
	}	

	private void OnError()
	{
	}
}
