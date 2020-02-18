using System.Collections.Generic;
using I2.Loc;
using JunkyardDogs.Data;
using PandeaGames;
using PandeaGames.Runtime.Dialogs.ViewModels;
using PandeaGames.Views;
using UnityEngine;

namespace JunkyardDogs.Views
{
    public class ChooseNationlaityView : AbstractUnityView
    {
        private ChooseNationalityViewModel _vm;
        private List<MessageDialogViewModel.Option> _chooseOptions;
        
        public override void Show()
        {
            FindWindow().LaunchScreen("chooseCountry");
            _vm = Game.Instance.GetViewModel<ChooseNationalityViewModel>(0);
            _vm.OnRequestNationChange += VmOnRequestNationChange;
            _chooseOptions = new List<MessageDialogViewModel.Option>()
            {
                new MessageDialogViewModel.Option("UI.ok"),
                new MessageDialogViewModel.Option("UI.cancel")
            };
        }

        public override void Destroy()
        {
            base.Destroy();
            _vm.OnRequestNationChange -= VmOnRequestNationChange;
            _vm = null;
        }

        private NationalityStaticDataReference _obj;
        private void VmOnRequestNationChange(NationalityStaticDataReference obj)
        {
            _obj = obj;
            MessageDialogViewModel model = Game.Instance.GetViewModel<MessageDialogViewModel>();
            model.SetOptions(_chooseOptions, LocalizationManager.GetTranslation("screen.choose_nation.confirm_msg"));
            
            model.OnOptionSelected += ModelOnOptionSelected;
            
            FindServiceManager().GetService<DialogService>().DisplayDialog<UserDialog>(model);
        }

        private void ModelOnOptionSelected(MessageDialogViewModel.Option selected)
        {
            if (selected == _chooseOptions[0])
            {
                _vm.SetChosenNationality(_obj);
            }
        }
    }
}