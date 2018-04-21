using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Components;
using System;
using WeakReference = Data.WeakReference;

[Serializable]
public class JunkyardUser : User
{
    public int TEST { get; set; }
    public List<GenericComponent> Components { get; set; }
    public GenericComponent LastComponentAdded { get; set; }

    public void AddComponent(GenericComponent component)
    {
        foreach(GenericComponent comp in Components)
        {
            bool doTheyEqual = false;
            doTheyEqual = comp == LastComponentAdded;
            Debug.Log("Are they the same?"+ doTheyEqual);
        }

        Components.Add(component);
        LastComponentAdded = component;
    }

    public JunkyardUser()
    {
        TEST = 08976;
        Components = new List<GenericComponent>();
    }
}
