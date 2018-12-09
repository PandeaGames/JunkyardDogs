using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;
using JunkyardDogs.Components;
using JunkyardDogs.Simulation;
using PandeaGames;
using UnityEngine.SceneManagement;

public class TournamentController : MonoBehaviour 
{
	[SerializeField]
	private ServiceManager _serviceManager;
	
	private WeakReference _tournament;
	private JunkyardUserService _userService;
	private Engagement _engagement;
	private TournamentState.TournamentStatus _tournamentStatus;
	private MatchController _matchController;
	private TournamentState _tournamentState;
	private RoundState _roundState;
	
	public void RunTournament(RoundState roundState, Tournament tournament)
	{
		_userService = Game.Instance.GetService<JunkyardUserService>();

		JunkyardUser user = _userService.User;
		
		_roundState = roundState;
		ParticipantDataUtils.GenerateParticipantsAsync(tournament.Participants, (participants) =>
		{
			StartCoroutine(CompleteParticipantsCoroutine(participants, () => { }));
		}, OnError);
		StartRound(roundState);
	}
	
	public void RunTournament(TournamentState tournamentState)
	{
		_userService = Game.Instance.GetService<JunkyardUserService>();

		JunkyardUser user = _userService.User;
		
		_tournamentState = tournamentState;
		StartTournament(tournamentState);
	}
	
	public void RunTournament(WeakReference tournament)
	{
		_tournament = tournament;
		
		_tournament.LoadAssetAsync<Tournament>(TournamentLoaded, (e) => { });
	}
	
	private void TournamentLoaded(Tournament tournament, WeakReference reference)
	{
		_tournamentState =  tournament.GenerateState();
        
		ParticipantDataUtils.GenerateParticipantsAsync(tournament.Participants, (participants) =>
			{
				StartCoroutine(CompleteParticipantsCoroutine(participants, () => { }));
				_tournamentState.FillWithParticipants(participants);
				RunTournament(_tournamentState);
			}, OnError);
	}

	private IEnumerator CompleteParticipantsCoroutine(List<Participant> participants, Action onComplete)
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
				
				/*chooseController.ChooseBot(bot => { chosenBot = bot; }, () =>
				{
					hasError = true;
				}, () => { hasError = true; });*/

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

		if (_tournamentState == null)
		{
			_tournamentState.FillWithParticipants(participants);
			RunTournament(_tournamentState);
		}
		else
		{
			_tournamentState.FillWithParticipants(participants);
			RunTournament(_tournamentState);
		}

		onComplete();
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

	private IEnumerator LoadMatchSceneCoroutine(RoundState round)
	{
		return LoadMatchSceneCoroutine(round.GetCurrentMatch());
	}

	private IEnumerator LoadMatchSceneCoroutine(TournamentState.TournamentStatus status)
	{
		return LoadMatchSceneCoroutine(status.Match);
	}

	private IEnumerator LoadMatchSceneCoroutine(MatchState match)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("client/scenes/gameScenes/Match", LoadSceneMode.Additive);

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}

		_matchController = FindObjectOfType<MatchController>();
		//_matchController.RunMatch(match, OnMatchComplete);
	}

	private void OnMatchComplete(MatchOutcome outcomed)
	{
		outcomed.Match.Winner.Participant = outcomed.Winner;
		outcomed.Match.Loser.Participant = outcomed.Loser;

		if (_tournamentState == null ? _roundState.IsComplete():_tournamentState.IsComplete())
		{
			GameObject.Destroy(_matchController.gameObject);
			//StartCoroutine(UnloadMatchSceneCoroutine(() => { }));
		}
		else
		{
			GameObject.Destroy(_matchController.gameObject);
			if (_tournamentState == null)
			{
				StartRound(_roundState);
			}
			else
			{
				StartTournament(_tournamentState);
			}
			
			/*StartCoroutine(UnloadMatchSceneCoroutine(() =>
			{
				//StartTournament(_tournamentState);
			}));*/
		}
	}
	
	private void StartRound(RoundState round)
	{
		if (round.IsComplete())
		{
			Debug.Log("WINNER");
		}
		else
		{
			StartCoroutine(LoadMatchSceneCoroutine(round));
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
