using UnityEngine;
using System.Collections;
using Data;
using System;
using System.Collections.Generic;
using JunkyardDogs.Data;
using WeakReference = PandeaGames.Data.WeakReferences.WeakReference;

[Serializable]
public class Competitor
{
    [SerializeField, NationalityStaticDataReference]
    private NationalityStaticDataReference _nationality;
    
    [SerializeField]
    private Inventory _inventory;
    
    [SerializeField]
    private Record _record;
    
    public NationalityStaticDataReference Nationality
    {
        get { return _nationality;}
        set { _nationality = value; }
    }

    public Inventory Inventory
    {
        get { return _inventory;}
        set { _inventory = value; }
    }

    public Record Record
    {
        get { return _record;}
        set { _record = value; }
    }

    public Competitor()
    {
        Inventory = new Inventory();
        Record = new Record();
        Nationality = new NationalityStaticDataReference();
    }
}