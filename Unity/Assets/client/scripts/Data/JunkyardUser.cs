using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;
using System;
using WeakReference = Data.WeakReference;
using Component = JunkyardDogs.Components.Component;

[Serializable]
public class JunkyardUser : User
{
    public int Cash { get; set; }
    public WeakReference Nationality { get; set; }
    public List<Component> Components { get; set; }

    public void AddComponent(Component component)
    {
        Components.Add(component);
    }

    public JunkyardUser()
    {
        Components = new List<Component>();
    }
}