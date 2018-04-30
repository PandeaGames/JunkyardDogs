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
    public Competitor Competitor { get; set; }

    public void AddComponent(Component component)
    {
        Competitor.Inventory.AddComponent(component);
    }

    public JunkyardUser()
    {
        Competitor = new Competitor();
    }
}