using JunkyardDogs.Components;
using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames;
using PandeaGames.Views;
using PandeaGames.Views.ViewControllers;

namespace JunkyardDogs.Views
{
    public class JunkyardView : AbstractUnityView
    {
        private JunkyardViewModel _junkyardViewModel;
        private JunkyardUserViewModel _userModel;
        
        public override void InitializeView(IViewController controller)
        {
            base.InitializeView(controller);

            _userModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
            _junkyardViewModel = Game.Instance.GetViewModel<JunkyardViewModel>(0);
            
        }

        public override void Show()
        {
            FindWindow().LaunchScreen("junkyard");
            _junkyardViewModel.OnTakeJunk += OnTakeJunk;
        }

        public override void Destroy()
        {
            base.Destroy();
            
            _junkyardViewModel.OnTakeJunk -= OnTakeJunk;
        }

        private void OnTakeJunk(Component component)
        {
            TakeJunkDialogViewModel vm = Game.Instance.GetViewModel<TakeJunkDialogViewModel>();
            vm.SetData(new TakeJunkDialogViewModel.Data(component));
            vm.OnClose += OnTakeJunkClose; 
            FindServiceManager().GetService<DialogService>().DisplayDialog<TakeJunkDialog>(vm);
        }

        private void OnTakeJunkClose(TakeJunkDialogViewModel dialog)
        {
            if (dialog.ShouldTakeComponent)
            {
                _userModel.UserData.AddComponent(dialog.ModelData.Component);
            }
        }
    }
}