using PandeaGames;
using PandeaGames.Views;
using PandeaGames.Views.Screens;
using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames.Data.WeakReferences;

namespace JunkyardDogs.Views
{
    public class GarageEditBehaviourView : AbstractUnityView
    {
        private EditBotBehaviourViewModel _viewModel;
        
        public GarageEditBehaviourView()
        {
            _viewModel = Game.Instance.GetViewModel<EditBotBehaviourViewModel>(0);
            _viewModel.OnSelectNewDirective += OnSelectNewDirective;
        }
        
        public override void Show()
        {
            FindWindow().LaunchScreen("EditBotBehaviour");
        }

        private void OnSelectNewDirective()
        {
            //TODO: Select directive
            ChooseActionDialogViewModel vm = Game.Instance.GetViewModel<ChooseActionDialogViewModel>(0);

            vm.ActionList = _viewModel.ActionList;
            vm.OnClose += SelectedNewAction;
            FindServiceManager().GetService<DialogService>().DisplayDialog<ChooseActionDialog>(vm);
        }

        private void SelectedNewAction(ChooseActionDialogViewModel vm)
        {
            _viewModel.AdddNewAction(vm.Selection);
        }
    }
}