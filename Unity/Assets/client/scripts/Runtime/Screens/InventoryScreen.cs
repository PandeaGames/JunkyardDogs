using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;
using SRF.UI.Layout;
using Component = JunkyardDogs.Components.Component;

public class InventoryScreen : ScreenController
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

    public override void Setup(WindowController window, Config config)
    {
        base.Setup(window, config);

        _junkyardUserService = _serviceManager.GetService<JunkyardUserService>();
        _dialogService = _serviceManager.GetService<DialogService>();

        Inventory inventory = _junkyardUserService.User.Competitor.Inventory;

        foreach (Component component in inventory)
        {
            _virtualVerticalLayoutGroup.AddItem(component);
        }

            /*foreach(Component component in inventory)
            {
                ComponentDisplay display = Instantiate(_inventoryItemOriginal).GetComponent<ComponentDisplay>();
                display.transform.SetParent(_inventoryList, false);
                display.RenderComponent(component);
                display.gameObject.SetActive(true);

                Button button = display.GetComponent<Button>();

                if (button != null)
                {
                    button.onClick.AddListener(() => OnInventoryButtonPressed(component, display));
                }
            }*/
        }

    private void OnInventoryButtonPressed(Component component, ComponentDisplay display)
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
                Destroy(display.gameObject);
                _junkyardUserService.Save();
            }
        });
    }


}
