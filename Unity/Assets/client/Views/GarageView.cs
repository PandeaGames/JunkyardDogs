using JunkyardDogs.Components;
using PandeaGames;
using PandeaGames.Views;
using PandeaGames.Views.ViewControllers;
using JunkyardDogs.scripts.Runtime.Dialogs;

namespace JunkyardDogs.Views
{
    public class GarageView : SceneView
    {
        private GarageViewModel _viewModel;
        private EditBotBehaviourViewModel _behaviourViewModel;
        private DialogService _dialogService;

        public GarageView() : base("garage")
        {
            
        }
        
        public override void InitializeView(IViewController controller)
        {
            base.InitializeView(controller);
            _viewModel = Game.Instance.GetViewModel<GarageViewModel>(0);
            _behaviourViewModel = Game.Instance.GetViewModel<EditBotBehaviourViewModel>(0);

            _viewModel.OnNewBot += OnNewBotClicked;

            _dialogService = FindServiceManager().GetService<DialogService>();
        }

        public override void Destroy()
        {
            _viewModel = null;
        }
        
        public void OnNewBotClicked()
        {
            ChooseFromInventoryViewModel vm = Game.Instance.GetViewModel<ChooseFromInventoryViewModel>(0);
            vm.SetData(new ChooseFromInventoryViewModel.Data(typeof(Chassis),_viewModel.Data.User.Competitor.Inventory));
            vm.OnClose += SelectedNewChassis;
            _dialogService.DisplayDialog<ChooseFromInventoryDialog>(vm);
        }
        
        private void SelectedNewChassis(ChooseFromInventoryViewModel vm)
        {
            var choice = vm.Selected as Chassis;
            vm.OnClose -= SelectedNewChassis;

            if(choice != null)
            {
                _viewModel.AddBuilder(choice);
            }
        }
    }
}