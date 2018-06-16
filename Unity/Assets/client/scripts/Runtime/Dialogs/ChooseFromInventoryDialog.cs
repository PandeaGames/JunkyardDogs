using UnityEngine;
using System.Collections;
using System;
using SRF.UI.Layout;
using UnityEngine.UI;
using System.Collections.Generic;
using Component = JunkyardDogs.Components.Component;

public class ChooseFromInventoryDialog : Dialog
{
    public class ChooseFromInventoryDialogResponse : Response
    {
        private Component _component;

        public Component Component { get { return _component; } }

        public ChooseFromInventoryDialogResponse(Component component) : base()
        {
            _component = component;
        }

        public ChooseFromInventoryDialogResponse()
        {

        }
    }

    private interface IInventoryTypeSelector
    {
        List<Component> GetComponentsOfType();
    }

    [Serializable]
    public class ChooseFromInventoryDialogConfig<T> : Config, IInventoryTypeSelector where T: JunkyardDogs.Components.Component
    {
        public Inventory Inventory;

        public ChooseFromInventoryDialogConfig(Inventory inventory) : base()
        {
            this.Inventory = inventory;
        }

        public List<Component> GetComponentsOfType()
        {
            List<T> componentList = Inventory.GetComponentsOfType<T>();
            List<Component> baseComponentResults = new List<Component>();
            componentList.ForEach((component) => baseComponentResults.Add((Component)component));
            return baseComponentResults;
        }
    }

    [SerializeField]
    private VirtualVerticalLayoutGroup _virtualVerticalLayoutGroup;

    private IInventoryTypeSelector _config;

    public override void Setup(Config config, DialogResponseDelegate responseDelegate = null)
    {
        base.Setup(config, responseDelegate);
        _config = config as IInventoryTypeSelector;
        List<Component> components = _config.GetComponentsOfType();
        components.ForEach((component) => _virtualVerticalLayoutGroup.AddItem(component));
    }

    protected override Response GenerateResponse()
    {
        return new ChooseFromInventoryDialogResponse(_virtualVerticalLayoutGroup.SelectedItem as Component);
    }
}
