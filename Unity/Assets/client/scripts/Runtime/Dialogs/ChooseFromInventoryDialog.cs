using UnityEngine;
using UnityEngine.UI;
using JunkyardDogs.Components;

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

        private void ListViewOnItemSelected(IComponent data)
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