using PandeaGames.Views.ViewControllers;

namespace JunkyardDogs
{
	public enum MatchTestViewStates
	{
		Preloading,
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
				_fsm.SetState(MatchTestViewStates.Match);
			}
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
			SetViewStateController<MatchTestState>(MatchTestViewStates.Match);
            
			SetInitialState(MatchTestViewStates.Preloading);
		}
	}
}