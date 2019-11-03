using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Component = JunkyardDogs.Components.Component;

public class ComponentListItem : AbstractListItem<Component>
{
    [SerializeField]
    private ComponentDisplay _componentDisplay;

    public override void SetData(Component data)
    {
        base.SetData(data);
        _componentDisplay.RenderComponent(data);
    }
}
