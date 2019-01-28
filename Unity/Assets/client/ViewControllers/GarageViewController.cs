using JunkyardDogs.Data;
using PandeaGames.Views;
using PandeaGames.Views.ViewControllers;
using JunkyardDogs.Views;
using PandeaGames;

namespace JunkyardDogs
{
    public enum GarageViewsStates
    {
        Lineup, 
        Focused,
        EditBehaviour
    }
    
    public class GarageViewController : AbstractViewControllerFsm<GarageViewsStates>
    {
        public class GarageLineupState : AbstractViewControllerState<GarageViewsStates>
        {
            private GarageViewModel _garageViewModel;

            public GarageLineupState()
            {
                _garageViewModel = Game.Instance.GetViewModel<GarageViewModel>(0);
            }
            
            protected override IViewController GetViewController()
            {
                GarageLineupViewController vc = new GarageLineupViewController();
                return vc;
            }

            public override void UpdateState()
            {
                base.UpdateState();

                if (_garageViewModel.FocusedBuilder != null)
                {
                    _fsm.SetState(GarageViewsStates.Focused);
                }
            }
        }
        
        public class GarageFocusedState : AbstractViewControllerState<GarageViewsStates>
        {
            private GarageViewModel _garageViewModel;
            
            public GarageFocusedState()
            {
                _garageViewModel = Game.Instance.GetViewModel<GarageViewModel>(0);
            }
            
            protected override IViewController GetViewController()
            {
                GarageFocusedViewController vc = new GarageFocusedViewController();
                return vc;
            }

            public override void EnterState(GarageViewsStates @from)
            {
                base.EnterState(@from);
                
                _garageViewModel.OnEditBehaviour += OnEditBehaviour;
            }

            public override void LeaveState(GarageViewsStates to)
            {
                base.LeaveState(to);
                _garageViewModel.OnEditBehaviour -= OnEditBehaviour;
            }

            private void OnEditBehaviour()
            {
                _fsm.SetState(GarageViewsStates.EditBehaviour);
            }

            public override void UpdateState()
            {
                base.UpdateState();

                if (_garageViewModel.FocusedBuilder == null)
                {
                    _fsm.SetState(GarageViewsStates.Lineup);
                }
            }
        }
        
        public class GarageEditBehaviourState : AbstractViewControllerState<GarageViewsStates>
        {
            private EditBotBehaviourViewModel _behaviourViewModel;
            private GarageViewModel _garageViewModel;

            public GarageEditBehaviourState()
            {
                _garageViewModel = Game.Instance.GetViewModel<GarageViewModel>(0);
                _behaviourViewModel = Game.Instance.GetViewModel<EditBotBehaviourViewModel>(0);
                _behaviourViewModel.ActionList = Game.Instance.GetStaticDataPovider<GameStaticDataProvider>()
                    .GameDataStaticData.ActionListRef;
            }
            
            protected override IViewController GetViewController()
            {
                GarageEditBehaviourViewController vc = new GarageEditBehaviourViewController();
                return vc;
            }

            public override void LeaveState(GarageViewsStates to)
            {
                base.LeaveState(to);
                
                _behaviourViewModel.OnDone -= OnDone;
            }

            public override void EnterState(GarageViewsStates @from)
            {
                _behaviourViewModel.OnDone += OnDone;
                _behaviourViewModel.Bot = _garageViewModel.FocusedBuilder.Bot;      
                base.EnterState(@from);
            }

            private void OnDone()
            {
                _fsm.SetState(GarageViewsStates.Focused);
            }
        }
        
        private GarageViewModel _viewModel;
        
        public GarageViewController()
        {
            SetViewStateController<GarageEditBehaviourState>(GarageViewsStates.EditBehaviour);
            SetViewStateController<GarageFocusedState>(GarageViewsStates.Focused);
            SetViewStateController<GarageLineupState>(GarageViewsStates.Lineup);
            SetInitialState(GarageViewsStates.Lineup);
            _viewModel = Game.Instance.GetViewModel<GarageViewModel>(0);
        }

        public override void ShowView()
        {
            base.ShowView();
            _viewModel.SetData(new GarageViewModel.GarageData(Game.Instance.GetService<JunkyardUserService>().Load()));
        }

        public override void Initialize(IViewController parent)
        {
            base.Initialize(parent);
            
        }
        
        
        
        protected override IView CreateView()
        {
            return new GarageView();
        }
    }
}