using UnityEngine;
using System.Collections;
using System;
using JunkyardDogs.Specifications;
using WeakReference = Data.WeakReference;

[Serializable]
[CreateAssetMenu(fileName = "SpecificationCatalogue", menuName = "GamePlay/SpecificationCatalogue", order = 3)]
public class SpecificationCatalogue : ScriptableObject
{
    [SerializeField]
    private Manufacturer _manufacturer;

    public Manufacturer Manufacturer
    {
        get
        {
            return _manufacturer;
        }
    }

    [SerializeField]
    private WeakReference[] _specifications;

    public WeakReference[] Specifications
    {
        get
        {
            return _specifications;
        }
    }
}
