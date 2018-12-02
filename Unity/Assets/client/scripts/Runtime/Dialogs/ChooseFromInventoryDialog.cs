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
    private VirtualVerticalLayoutGroup _virtualVerticalLayoutGroup;

    protected override void Initialize()
    {
        
        foreach (var component in _viewModel.modelData.Inventory.GetComponentsOfType(_viewModel.modelData.Type))
        {
            _virtualVerticalLayoutGroup.AddItem(component);
        }
        
        _virtualVerticalLayoutGroup.SelectedItemChanged.AddListener((data)=> _viewModel.Selected = data as Component);
    }
}
}