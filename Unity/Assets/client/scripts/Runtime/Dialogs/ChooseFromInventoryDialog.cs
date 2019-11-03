using UnityEngine;
using System.Collections;
using System;
using SRF.UI.Layout;
using UnityEngine.UI;
using System.Collections.Generic;
using Component = JunkyardDogs.Components.Component;

namespace JunkyardDogs.scripts.Runtime.Dialogs
{
    public class ChooseFromInventoryDialog : Dialog<ChooseFromInventoryViewModel>
    {
        [SerializeField]
        private Button _removeComponentButton;

        [SerializeField]
        private ComponentListView _listView;

        protected override void Initialize()
        {
            _removeComponentButton.onClick.AddListener(OnRemoveComponent);
            /*foreach (var component in _viewModel.modelData.Inventory.GetComponentsOfType(_viewModel.modelData.Type))
            {
                _virtualVerticalLayoutGroup.AddItem(component);
            }*/
            
            _listView.SetData(_viewModel.modelData.Inventory.GetComponentsOfType(_viewModel.modelData.Type));
            _listView.OnItemSelected += ListViewOnItemSelected;
            
            //_virtualVerticalLayoutGroup.SelectedItemChanged.AddListener((data)=> _viewModel.Selected = data as Component);
        }

        private void ListViewOnItemSelected(Component data)
        {
            _viewModel.Selected = data;
            Close();
        }

        private void OnRemoveComponent()
        {
            _viewModel.Selected = null;
            Close();
        }
    }
}