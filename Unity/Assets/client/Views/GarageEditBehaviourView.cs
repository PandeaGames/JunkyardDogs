using JunkyardDogs.Components;
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
        private JunkyardUserViewModel _userViewModel;

        
        public GarageEditBehaviourView()
        {
            _viewModel = Game.Instance.GetViewModel<EditBotBehaviourViewModel>(0);
            _viewModel.OnSelectNewDirective += OnSelectNewDirective;
            _viewModel.OnSwapCPU += VmOnSwapCpu;
            _viewModel.OnChooseDirective += ViewModelOnChooseDirective;
            _userViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
        }

        private void ViewModelOnChooseDirective(int obj)
        {
            ChooseFromInventoryViewModel dialogVM = Game.Instance.GetViewModel<ChooseFromInventoryViewModel>();
            dialogVM.SetData(new ChooseFromInventoryViewModel.Data(typeof(Directive), _userViewModel.UserData.Competitor.Inventory));
            
            FindServiceManager().GetService<DialogService>().DisplayDialog<ChooseFromInventoryDialog>(dialogVM);
            
            dialogVM.OnClose += (dialog) =>
            {
                _viewModel.Bot.CPU.SetDirective(obj, dialog.Selected as Directive);
                _viewModel.DirectiveChosen(obj);
            };
        }

        private void VmOnSwapCpu()
        {
            ChooseFromInventoryViewModel dialogVM = Game.Instance.GetViewModel<ChooseFromInventoryViewModel>();
            dialogVM.SetData(new ChooseFromInventoryViewModel.Data(typeof(CPU), _userViewModel.UserData.Competitor.Inventory));
            dialogVM.OnClose += DialogVmOnClose;
            FindServiceManager().GetService<DialogService>().DisplayDialog<ChooseFromInventoryDialog>(dialogVM);
        }

        private void DialogVmOnClose(ChooseFromInventoryViewModel dialog)
        {
            _viewModel.Bot.CPU = dialog.Selected as CPU;
            _viewModel.CPUSwapped();
        }
        
        public override void Show()
        {
            FindWindow().LaunchScreen("EditBotBehaviour");
        }

        public override void Destroy()
        {
            _viewModel.OnSelectNewDirective -= OnSelectNewDirective;
            _viewModel.OnSwapCPU -= VmOnSwapCpu;
        }

        private void OnSelectNewDirective()
        {
            //TODO: Select directive
            ChooseActionDialogViewModel vm = Game.Instance.GetViewModel<ChooseActionDialogViewModel>(0);
            FindServiceManager().GetService<DialogService>().DisplayDialog<ChooseActionDialog>(vm);
        }
    }
}