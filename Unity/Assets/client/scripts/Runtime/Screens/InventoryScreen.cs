using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PandeaGames;
using PandeaGames.Views.Screens;
using SRF.UI.Layout;
using Component = JunkyardDogs.Components.Component;

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

        foreach (Component component in inventory)
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
            OnInventoryButtonPressed(data as Component);
        }
    }

    private void OnInventoryButtonPressed(Component component)
    {
        List<MessageDialog.Option> options = new List<MessageDialog.Option>();

        MessageDialog.Option sellOption = new MessageDialog.Option("Sell");
        MessageDialog.Option cancel = new MessageDialog.Option("Cancel");

        options.Add(sellOption);
        options.Add(cancel);

        ComponentMessageDialog.ComponentMessageDialogConfig config = new ComponentMessageDialog.ComponentMessageDialogConfig(options);
        config.Component = component;

        _dialogService.DisplayDialog<ComponentMessageDialog>(config, (response) =>
        {
            MessageDialog.MessageDialogResponse messageResponse = response as MessageDialog.MessageDialogResponse;

            if(messageResponse.Choice == sellOption)
            {
                //TODO: Sell
                _junkyardUserService.User.Competitor.Inventory.Components.Remove(component);
                _virtualVerticalLayoutGroup.RemoveItem(component);
                _junkyardUserService.Save();
            }
        });
    }
}
