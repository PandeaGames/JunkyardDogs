using UnityEngine;
using System.Collections;
using Data;
using System;
using System.Collections.Generic;
using WeakReference = Data.WeakReference;

[Serializable]
public class Competitor
{
    [SerializeField]
    private WeakReference _nationality;

    public WeakReference Nationality { get { return _nationality; } set { _nationality = value; } }
    public Inventory Inventory { get; set; }
    public Record Record { get; set; }

    public Competitor()
    {
        Inventory = new Inventory();
        Record = new Record();
    }
}