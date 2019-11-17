using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;
using PandeaGames;
using PandeaGames.Views.Screens;
using SRF.UI.Layout;
using PandeaGames.Runtime.Dialogs.ViewModels;
using JunkyardDogs.scripts.Runtime.Dialogs;

public class InventoryScreen : ScreenView
{
    [SerializeField]
    private GameObject _inventoryItemOriginal;

    [SerializeField]
    private Transform _inventoryList;

    [SerializeField]
    private ServiceManager _serviceManager;

    [SerializeField]
    private VirtualVerticalLayoutGroup _virtualVerticalLayoutGroup;

    private JunkyardUserService _junkyardUserService;
    private DialogService _dialogService;

    public override void Setup(WindowView window)
    {
        base.Setup(window);

        _junkyardUserService = Game.Instance.GetService<JunkyardUserService>();
        _dialogService = _serviceManager.GetService<DialogService>();

        Inventory inventory = _junkyardUserService.User.Competitor.Inventory;

        foreach (IComponent component in inventory)
        {
            _virtualVerticalLayoutGroup.AddItem(component);
        }
        RectTransform rt = _virtualVerticalLayoutGroup.transform as RectTransform;
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, _virtualVerticalLayoutGroup.minHeight);
        _virtualVerticalLayoutGroup.SelectedItemChanged.AddListener(OnInventoryButtonPressed);
    }

    private void OnDestroy()
    {
        _virtualVerticalLayoutGroup.SelectedItemChanged.RemoveListener(OnInventoryButtonPressed);
    }

    public void OnInventoryButtonPressed(object data)
    {
        if(data != null)
        {
            OnInventoryButtonPressed(data as IComponent);
        }
    }

    private void OnInventoryButtonPressed(IComponent component)
    {
        List<MessageDialogViewModel.Option> options = new List<MessageDialogViewModel.Option>();

        MessageDialogViewModel.Option sellOption = new MessageDialogViewModel.Option("Sell");
        MessageDialogViewModel.Option cancel = new MessageDialogViewModel.Option("Cancel");

        options.Add(sellOption);
        options.Add(cancel);

        ComponentViewModel vm = Game.Instance.GetViewModel<ComponentViewModel>(0);
        
        vm.SetOptions(options);
        vm.OnOptionSelected +=(option) =>
        {
            if(option == sellOption)
            {
                //TODO: Sell
                _junkyardUserService.User.Competitor.Inventory.Components.Remove(component);
                _virtualVerticalLayoutGroup.RemoveItem(component);
                _junkyardUserService.Save();
            }
        };
    }
}
