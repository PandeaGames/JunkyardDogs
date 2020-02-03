using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;

[Serializable]
public class Inventory : IEnumerable
{
    [SerializeField] private List<IComponent> _components;
    [SerializeField] private List<Bot> _bots;
    
    public List<IComponent> Components
    {
        get => _components;
        set => _components = value;
    }
    
    public List<Bot> Bots 
    {
        get => _bots;
        set => _bots = value;
    }

    public Inventory()
    {
        Components = new List<IComponent>();
        Bots = new List<Bot>();
    }

    public IComponent AddComponent(IComponent component)
    {
        Components.Add(component);
        return component;
    }

    public bool ContainsComponent(IComponent component)
    {
        return Components.Contains(component);
    }

    public void RemoveComponent(IComponent component)
    {
        Components.Remove(component);
    }

    public IEnumerable<T> GetComponentsOfType<T>() where T : IComponent
    {
        foreach (var component in Components)
        {
            if (component is T)
                yield return (T) component;
        }
    }
    
    public IEnumerable<IComponent> GetComponentsOfType(Type type)
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

    public IConsumable AddBot(Bot bot)
    {
        if(Bots == null)
        {
            Bots = new List<Bot>();
        }

        Bots.Add(bot);
        return bot;
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
    public List<IComponent> GetComponents()
    {
        return Components;
    }
#endif
}
