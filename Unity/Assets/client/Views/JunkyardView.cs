using I2.Loc;
using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames;
using PandeaGames.Runtime.Dialogs.ViewModels;
using PandeaGames.Views;
using PandeaGames.Views.ViewControllers;
using UnityEngine;

namespace JunkyardDogs.Views
{
    public class JunkyardView : AbstractUnityView
    {
        private JunkyardViewModel _junkyardViewModel;
        private GameObject _junkyardViewGO;
        private JunkyardMonoView _junkyardView;
        private JunkyardUserViewModel _userModel;
        private Junkyard _junkyard;
        private CameraViewModel _cameraViewModel;
        private JunkyardUserViewModel _junkyardUserViewModel;
        
        public override void InitializeView(IViewController controller)
        {
            base.InitializeView(controller);

            _userModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
            _junkyardViewModel = Game.Instance.GetViewModel<JunkyardViewModel>(0);
            _cameraViewModel = Game.Instance.GetViewModel<CameraViewModel>(0);
            
            _junkyardViewGO = GameObject.Instantiate(Game.Instance.GetStaticDataPovider<GameStaticDataProvider>().GameDataStaticData.JunkyardView);
            _junkyardViewGO.SetActive(false);
            _junkyardView = _junkyardViewGO.GetComponent<JunkyardMonoView>();
            _junkyardUserViewModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
        }

        public override void Show()
        {
            FindWindow().LaunchScreen("junkyard");
            _junkyardViewModel.OnTakeJunk += OnTakeJunk;
            _junkyardViewGO.SetActive(true);
            
            Game.Instance.GetService<JunkyardUserService>().Load();
            
            if (_junkyardUserViewModel.UserData.Competitor.Inventory.Bots.Count <= 0)
            {
                MessageDialogViewModel vm = new MessageDialogViewModel();
                vm.SetOptions(
                    new MessageDialogViewModel.Option("UI.ok"),
                    LocalizationManager.GetTranslation("UI.dialog.bot_required"));
        
                FindServiceManager().GetService<DialogService>().DisplayDialog<MessageDialog>(vm);
            }
            else
            {
                Bot bot = _junkyardUserViewModel.UserData.Competitor.Inventory.Bots[0];
        
                _junkyardView.Render(_junkyardViewModel, bot);
            }
        }

        public override void Destroy()
        {
            base.Destroy();
            GameObject.Destroy(_junkyardViewGO);
            _junkyardViewModel.OnTakeJunk -= OnTakeJunk;
        }

        private void OnTakeJunk(ILoot[] loot, Vector3 vector)
        {
            _userModel.Consume(loot, UnityEngine.Time.frameCount, _cameraViewModel.ActiveMaster.Camera.WorldToScreenPoint(vector));
        }
    }
}