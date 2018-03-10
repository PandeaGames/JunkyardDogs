using UnityEngine;
using System.Collections;
using JunkyardDogs.Components;
using JunkyardDogs.Specifications;

[CreateAssetMenu(fileName = "ComponentFactory", menuName = "GamePlay/ComponentFactory", order = 3)]
public class ComponentFactory : ScriptableObject
{
    public JunkyardDogs.Components.GenericComponent GenerateComponent(Specification spec)
    {
        JunkyardDogs.Components.GenericComponent component = ScriptableObject.CreateInstance<JunkyardDogs.Components.GenericComponent>();

        //component.Specification = spec;

        return component;
    }
}