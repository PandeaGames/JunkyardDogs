using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junk : MonoBehaviour {

    public delegate void JunkDelegate(Junk junk);

    public JunkDelegate OnClick;

    [SerializeField]
    private SpecificationCatalogue _catalogue;

    public SpecificationCatalogue Catalogue
    {
        get
        {
            return _catalogue;
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
