using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;
using Component = JunkyardDogs.Components.Component;

public class Inventory : IEnumerable
{
    public List<Component> Components { get; set; }
    public List<Bot> Bots { get; set; }

    public void AddComponent(Component component)
    {
        Components.Add(component);
    }

    public bool ContainsComponent(Component component)
    {
        return Components.Contains(component);
    }

    public void RemoveComponent(Component component)
    {
        Components.Remove(component);
    }

    public List<T> GetComponentsOfType<T>() where T : Component
    {
        List<T> results = new List<T>();

        Components.ForEach((component) => {
            if (component is T)
                results.Add(component as T);
                });

        return results;
    }

    public IEnumerator GetEnumerator()
    {
        return Components.GetEnumerator();
    }

    public void AddBot(Bot bot)
    {
        if(Bots == null)
        {
            Bots = new List<Bot>();
        }

        Bots.Add(bot);
    }

    public void DismantleBot(Bot bot)
    {
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
