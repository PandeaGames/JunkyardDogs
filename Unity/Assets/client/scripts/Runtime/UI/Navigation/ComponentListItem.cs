using JunkyardDogs.Components;
using UnityEngine;

public class ComponentListItem : AbstractListItem<IComponent>
{
    [SerializeField]
    private ComponentDisplay _componentDisplay;

    public override void SetData(IComponent data)
    {
        base.SetData(data);
        _componentDisplay.RenderComponent(data);
    }
}
