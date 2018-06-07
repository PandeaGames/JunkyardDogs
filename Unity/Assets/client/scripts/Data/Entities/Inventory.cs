using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;
using Component = JunkyardDogs.Components.Component;

public class Inventory : IEnumerable
{
    public List<Component> Components { get; set; }

    public void AddComponent(Component component)
    {
        Components.Add(component);
    }

    public IEnumerator GetEnumerator()
    {
        return Components.GetEnumerator();
    }

    public Inventory()
    {
        Components = new List<Component>();
    }

#if UNITY_EDITOR
    public List<Component> GetComponents()
    {
        return Components;
    }
#endif
}
