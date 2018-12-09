using Data;
using JunkyardDogs.Data;
using JunkyardDogs.Simulation;
using PandeaGames;
using PandeaGames.Views.ViewControllers;

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
		protected override IViewController GetViewController()
		{
			ContainerViewController vc = new ContainerViewController(
				new JunkyardViewController(),
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