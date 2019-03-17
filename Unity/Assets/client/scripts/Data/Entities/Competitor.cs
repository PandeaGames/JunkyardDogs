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
    public NationalityStaticDataReference Nationality { get; set;}
    public Inventory Inventory { get; set; }
    public Record Record { get; set; }

    public Competitor()
    {
        Inventory = new Inventory();
        Record = new Record();
        Nationality = new NationalityStaticDataReference();
    }
}