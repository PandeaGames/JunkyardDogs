using UnityEngine;
using System.Collections;
using Data;
using System.Collections.Generic;

public class Competitor
{
    public WeakReference Nationality { get; set; }
    public Inventory Inventory { get; set; }
    public Record Record { get; set; }

    public Competitor()
    {
        Inventory = new Inventory();
        Record = new Record();
    }
}