﻿using JunkyardDogs.Components;
using JunkyardDogs.Data;
using JunkyardDogs.scripts.Runtime.Dialogs;
using PandeaGames;
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
        
        public override void InitializeView(IViewController controller)
        {
            base.InitializeView(controller);

            _userModel = Game.Instance.GetViewModel<JunkyardUserViewModel>(0);
            _junkyardViewModel = Game.Instance.GetViewModel<JunkyardViewModel>(0);
            _junkyardViewModel.junkyard = JunkyardService.Instance.GetJunkyard(Game.Instance.GetStaticDataPovider<GameStaticDataProvider>().GameDataStaticData.JunkyardTestData);
            
            _junkyardViewGO = GameObject.Instantiate(Game.Instance.GetStaticDataPovider<GameStaticDataProvider>().GameDataStaticData.JunkyardView);
            _junkyardViewGO.SetActive(false);
            _junkyardView = _junkyardViewGO.GetComponent<JunkyardMonoView>();
        }

        public override void Show()
        {
            FindWindow().LaunchScreen("junkyard");
            _junkyardViewModel.OnTakeJunk += OnTakeJunk;
            _junkyardViewGO.SetActive(true);
            
            Game.Instance.GetService<JunkyardUserService>().Load();
            Bot bot = Game.Instance.GetService<JunkyardUserService>().User.Competitor.Inventory.Bots[0];
        
            _junkyardView.Render(_junkyardViewModel.junkyard, bot);
        }

        public override void Destroy()
        {
            base.Destroy();
            GameObject.Destroy(_junkyardViewGO);
            _junkyardViewModel.OnTakeJunk -= OnTakeJunk;
        }

        private void OnTakeJunk(ILoot[] loot)
        {
            TakeJunkDialogViewModel vm = Game.Instance.GetViewModel<TakeJunkDialogViewModel>();
            vm.SetData(new TakeJunkDialogViewModel.Data(loot));
            vm.OnClose += OnTakeJunkClose; 
            FindServiceManager().GetService<DialogService>().DisplayDialog<TakeJunkDialog>(vm);
        }

        private void OnTakeJunkClose(TakeJunkDialogViewModel dialog)
        {
            if (dialog.ShouldTakeLoot)
            {
                _userModel.UserData.Consume(dialog.ModelData.Loot, 0);
            }
        }
    }
}