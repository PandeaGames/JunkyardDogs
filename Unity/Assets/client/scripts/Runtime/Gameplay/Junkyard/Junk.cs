using System;
using System.Collections;
using System.Collections.Generic;
using JunkyardDogs.Data;
using UnityEngine;

public class Junk : MonoBehaviour {

    public delegate void JunkDelegate(Junk junk);

    public JunkDelegate OnClick;

    [SerializeField, LootCrateStaticDataReference]
    private LootCrateStaticDataReference _lootCrate;

    public LootCrateStaticDataReference LootCrate
    {
        get
        {
            return _lootCrate;
        }
    }

    [SerializeField]
    [Range(0, 1)]
    private float _availability;
    public float Availability
    {
        get
        {
            return _availability;
        }
    }

    public void HandleClick()
    {
        if (OnClick != null)
            OnClick(this);
    }
}
