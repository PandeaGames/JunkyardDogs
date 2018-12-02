using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames;
using PandeaGames.Views;

namespace JunkyardDogs.Views
{
    public class GarageFocusedView : AbstractUnityView
    {
        private GarageViewModel _garageViewModel;

        public GarageFocusedView()
        {
            _garageViewModel = Game.Instance.GetViewModel<GarageViewModel>(0);
            _garageViewModel.OnChooseFromInventory += OnChooseFromInventory;
        }
        
        public override void Show()
        {
            FindWindow().LaunchScreen("EditBot");  
        }

        public override void Destroy()
        {
            base.Destroy();
            _garageViewModel.OnChooseFromInventory -= OnChooseFromInventory;
        }

        private void OnChooseFromInventory(ChooseFromInventoryViewModel vm)
        {
            FindServiceManager().GetService<DialogService>().DisplayDialog<ChooseFromInventoryDialog>(vm);
        }
    }
}