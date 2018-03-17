using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;

public class JunkyardUser : User
{
    [SerializeField]
    private List<GenericComponent> _components = new List<GenericComponent>();

    public void AddComponent(GenericComponent component)
    {
        _components.Add(component);
    }
}
