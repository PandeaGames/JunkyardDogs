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
    [SerializeField]
    private int _cash;

    [SerializeField]
    private Competitor _competitor;

    public int Cash { get { return _cash; } set { _cash = value; } }
    public Competitor Competitor { get { return _competitor; } set { _competitor = value; } }

    public void AddComponent(Component component)
    {
        Competitor.Inventory.AddComponent(component);
    }

    public JunkyardUser()
    {
        Competitor = new Competitor();
    }
}