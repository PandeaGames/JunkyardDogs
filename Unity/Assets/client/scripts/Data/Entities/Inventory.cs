using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JunkyardDogs.Components;
using Component = JunkyardDogs.Components.Component;

public class Inventory : IEnumerable
{
    public List<Component> Components { get; set; }
    public List<Bot> Bots { get; set; }

    public Inventory()
    {
        
        Components = new List<Component>();
        Bots = new List<Bot>();
    }

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

    public IEnumerable<T> GetComponentsOfType<T>() where T : Component
    {
        foreach (var component in Components)
        {
            if (component is T)
                yield return component as T;
        }
    }
    
    public IEnumerable<Component> GetComponentsOfType(Type type)
    {
        foreach (var component in Components)
        {
            if (component != null && component.GetType().Equals(type))
                yield return component;
        }
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
        if (!Bots.Contains(bot))
        {
            return;
        }
        
        Bots.Remove(bot);
        bot.Chassis.Dismantle(this);
    }

#if UNITY_EDITOR
    public List<Component> GetComponents()
    {
        return Components;
    }
#endif
}
