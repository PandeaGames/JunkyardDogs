using Data;
using JunkyardDogs.Data;
using JunkyardDogs.Simulation;
using JunkyardDogs.Views;
using PandeaGames;
using PandeaGames.Views.ViewControllers;
using UnityEditor;

namespace JunkyardDogs
{
	public enum TournamentTestViewStates
	{
		Preloading,
		Tournament
	}
    
	public class TournamentPreloadState : AbstractViewControllerState<TournamentTestViewStates>
	{
		protected override IViewController GetViewController()
		{
			PreloadViewController vc = new PreloadViewController();
			vc.OnEnterState += PreloaderOnEnterState;
			return vc;
		}

		private void PreloaderOnEnterState(PreloadViewStates state)
		{
			if (state == PreloadViewStates.PreloadComplete)
			{
				_fsm.SetState(TournamentTestViewStates.Tournament);
			}
		}
	}

	public class TournamentTestState : AbstractViewControllerState<TournamentTestViewStates>
	{
		private MatchViewModel _vm;
		private TournamentViewModel _tournamentVm;

		public TournamentTestState()
		{
			_vm = Game.Instance.GetViewModel<MatchViewModel>(0);
			_tournamentVm = Game.Instance.GetViewModel<TournamentViewModel>(0);
		}
		
		
		public override void LeaveState(TournamentTestViewStates to)
		{
			base.LeaveState(to);
			_vm.OnMatchComplete -= VmOnMatchComplete;
		}

		public override void EnterState(TournamentTestViewStates @from)
		{
			base.EnterState(@from);
			_vm.OnMatchComplete += VmOnMatchComplete;
		}

		private void VmOnMatchComplete()
		{
			//_vm.Engagement.Outcome.Winner
			var state  = _tournamentVm.State;
			var status = state.GetStatus();
			//var match = status.Stage.Rounds[0].Matches[0].Winner;
			var winner = status.Stage.Rounds[0].Matches[0].Winner;
			
			Participant winnerP = winner.Participant;

			if (winnerP is SingleSourceParticipant)
			{
				#if UNITY_EDITOR
				EditorUtility.DisplayDialog("Winner!", (winnerP as SingleSourceParticipant).Source.ID, "OK");
				#endif
			}

		}

		protected override IViewController GetViewController()
		{
			ContainerViewController vc = new ContainerViewController(
				new JunkyardDogsGameViewController(),
				new TournemantViewController()
			);

			return vc;
		}
	}
    
	public class TournamentTestViewController : AbstractViewControllerFsm<TournamentTestViewStates>
	{		
		public TournamentTestViewController()
		{
			SetViewStateController<TournamentPreloadState>(TournamentTestViewStates.Preloading);
			SetViewStateController<TournamentTestState>(TournamentTestViewStates.Tournament);
			SetInitialState(TournamentTestViewStates.Preloading);
		}
	}
}