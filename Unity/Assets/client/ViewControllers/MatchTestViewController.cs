using Data;
using JunkyardDogs.Data;
using JunkyardDogs.Simulation;
using PandeaGames;
using PandeaGames.Views.ViewControllers;

namespace JunkyardDogs
{
	public enum MatchTestViewStates
	{
		Preloading,
		LoadMatch,
		Match
	}
    
	public class MatchTestPreloadState : AbstractViewControllerState<MatchTestViewStates>
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
				_fsm.SetState(MatchTestViewStates.LoadMatch);
			}
		}
	}

	public class MatchTestLoadState : AbstractViewControllerState<MatchTestViewStates>
	{
		private MatchTestViewModel _viewModel;
		private JunkyardUserViewModel _userViewModel;
		private MatchViewModel _matchViewModel;
		
		public MatchTestLoadState()
		{
			_viewModel = Game.Instance.GetViewModel<MatchTestViewModel>(0);
			_userViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
			_matchViewModel = Game.Instance.GetViewModel<MatchViewModel>(0);
		}
		
		public override void EnterState(MatchTestViewStates @from)
		{
			base.EnterState(@from);

			Engagement engagement = null;
			
			_viewModel.TestData.GetParticipantsAsync(_userViewModel.UserData, (participantTeams) =>
			{
				engagement = new Engagement();

				engagement.BlueCombatent = participantTeams[0].Bot;
				engagement.RedCombatent = participantTeams[1].Bot;
				engagement.SetTimeLimit(180);//3 minutes
            
				Loader loader = new Loader();
				loader.AppendProvider(engagement.BlueCombatent);
				loader.AppendProvider(engagement.RedCombatent);
				loader.LoadAsync(() =>
				{
					_matchViewModel.Engagement = engagement;
					_fsm.SetState(MatchTestViewStates.Match);
				}, (error) => { });
            
			}, () => { });
		}
	}

	public class MatchTestState : AbstractViewControllerState<MatchTestViewStates>
	{
		protected override IViewController GetViewController()
		{
			ContainerViewController vc = new ContainerViewController(
				new JunkyardViewController(),
				new MatchViewController()
			);

			return vc;
		}
	}
    
	public class MatchTestViewController : AbstractViewControllerFsm<MatchTestViewStates>
	{		
		public MatchTestViewController()
		{
			SetViewStateController<MatchTestPreloadState>(MatchTestViewStates.Preloading);
			SetViewStateController<MatchTestLoadState>(MatchTestViewStates.LoadMatch);
			SetViewStateController<MatchTestState>(MatchTestViewStates.Match);
            
			SetInitialState(MatchTestViewStates.Preloading);
		}
	}
}